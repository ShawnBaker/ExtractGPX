# ExtractGPX

Extracts the GPS data from an MP4 file and writes it to a GPX file.

# Usage

```
ExtractGPX mp4-file-name [gpx-file-name]
```

If the GPX file name is not specified, it will be created by replacing the MP4 file name extension with GPX. So
```
ExtractGPX MyVideo.mp4
```
would produce a file named __MyVideo.gpx__.

# Attributions

This project uses the following NuGet packages:
- [FFmpeg.AutoGen](https://www.nuget.org/packages/FFmpeg.AutoGen)
- [FrozenNorth.Gpmf](https://www.nuget.org/packages/FrozenNorth.Gpmf)
- [FrozenNorth.Gpx](https://www.nuget.org/packages/FrozenNorth.Gpx)

The icon is from the [IconMarketPK](https://www.flaticon.com/authors/iconmarketpk) package on FlatIcon.

# License

MIT License

Copyright © 2023 Shawn Baker
