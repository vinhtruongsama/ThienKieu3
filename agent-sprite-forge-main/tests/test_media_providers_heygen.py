"""HeyGen image-to-video adapter, exercised through the real route and mocked HTTPS transport only."""
from __future__ import annotations

import base64
import json

import pytest

from test_media_providers_common import (KEYS, assert_contracts, assert_no_secret, http, ledger_lines, media_mp4,
                                         media_providers, mp4, no_network, project, run_route, surfaces)

API = "https://api.heygen.com"
MEDIA = "https://media.heygen.com/signed/clip.mp4?token=temporary"
KEY = KEYS["HEYGEN_API_KEY"]


@pytest.fixture
def heygen_key(project, monkeypatch):
    monkeypatch.setenv("HEYGEN_API_KEY", KEY)
    monkeypatch.setattr(media_providers, "TODAY", media_providers.date(2026, 10, 28))


def test_sprite_clip_runs_through_heygen_submit_poll_download_and_ledger(project, http, heygen_key, capsys):
    http.on("POST", API + "/v3/models/videos", (200, {"data": {"video_id": "heygen-job-1"}}))
    http.on("GET", API + "/v3/models/videos/heygen-job-1",
            (200, {"data": {"status": "processing"}}),
            (200, {"data": {"status": "completed", "model": "heygen-video-1", "duration": 6,
                             "video_url": MEDIA}}))
    http.on("GET", MEDIA, (200, mp4()))

    code, result, err = run_route(
        capsys, "video", "--prompt-file", "prompt.txt", "--route", "heygen", "--reference", "master.png",
        "--last-frame", "master.png", "--duration", "6", "--resolution", "720p", "--out-dir", "out/heygen")
    assert code == 0, err
    assert result["route"] == "api:heygen" and result["model"] == "heygen-video-1"
    assert result["estimateUsd"] == pytest.approx(6 * 0.018)
    assert result["lastFrameUsed"] is False
    assert any("last frame is not pinned" in note for note in result["notes"])

    submit, = http.calls("POST")
    assert submit.headers["x-api-key"] == KEY
    body = submit.json()
    assert body == {
        "model": "heygen-video-1", "mode": "image_to_video", "prompt": "The same fox knight. Idle in place, facing LEFT. Flat #FF00FF.",
        "image": {"type": "base64", "media_type": "image/png",
                  "data": base64.b64encode((project / "master.png").read_bytes()).decode("ascii")},
        "duration": 6, "resolution": "768p", "prompt_enhancement": "disabled",
    }
    download = http.calls("GET")[-1]
    assert download.url == MEDIA and "x-api-key" not in download.headers

    folder = project / "out" / "heygen"
    assert (folder / "provider-download.mp4").read_bytes() == mp4()
    assert media_mp4.scan((folder / "generated.mp4").read_bytes())["tracks"] == ["vide"]
    job = json.loads((folder / "job.json").read_text(encoding="utf-8"))
    assert job["requestId"] == "heygen-job-1" and job["audio"]["removed"] == 1
    assert_contracts(job, project)
    assert ledger_lines(project)[-1]["reservedUsd"] == pytest.approx(0.108)
    assert_no_secret(json.dumps(result), err, *surfaces(project, "out/heygen"))


def test_heygen_resolver_exposes_it_as_video_only(project, heygen_key, capsys):
    code, result, err = run_route(capsys, "resolve", "--kind", "video", "--route", "heygen", "--resolution", "720p")
    assert code == 0, err
    assert result["route"] == "api:heygen" and result["model"] == "heygen-video-1"
    assert result["pinsLastFrame"] is False
    assert "heygen" not in media_providers.providers_for("image")
