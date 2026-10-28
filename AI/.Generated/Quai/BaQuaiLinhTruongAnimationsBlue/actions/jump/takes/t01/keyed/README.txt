video2dsprite output (provider-independent postprocessing)
==========================================================
frames-raw/         decoded source frames, frame_000000.png onwards (0-based)
frames-clean/       keyed RGBA frames, clean_0000.png onwards, plus matte-report.json
sprite/             sampled fixed-envelope sprites, strips, grids and preview GIFs
pipeline-meta.json  run metadata; its matte block is the matte report

Generation is separate: native tools, generate2dmedia API, or a supplied clip.
Fixed-envelope registration is the default. Inspect source contact drift
and loop seams; dense frames do not prove a seamless game-ready cycle.
Review keyed frames over light and dark backgrounds before packaging.
