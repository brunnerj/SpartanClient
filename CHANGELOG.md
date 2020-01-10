# Change Log for SpartanClient

## 1.0.0-beta [2020/1/10]

- Initial release after forking `[KaelusDenver/Spartan]`(https://github.com/KaelusDenver/Spartan)
and paring down the repository to support just the client application portion.

- Loads and compiles cleanly under LabVIEW 2011.

### Caveats

- The client application and library tree (`source\_Spartan Client Tree.vi`) should
load cleanly, and the client application should run (i.e., on a Windows 7 or later OS).
- The **Test** and **Driver** modules all load cleanly (no broken wires or missing VIs),
but the application currently doesn't have these inline (see `Spartan Client.vi`), so
operationally the application doesn't currently do very much.