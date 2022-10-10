# Ivao IT Aurora Helper
Project made with ❤️ by IVAO Italy division.

![GitHub milestone](https://img.shields.io/github/milestones/progress-percent/ivao-italy/Ivao.It.AuroraHelper/1) ![Website](https://img.shields.io/website?down_color=red&down_message=down&up_color=brightgreen&up_message=up&url=https%3A%2F%2Fdiscord.ivao.it) ![Discord](https://img.shields.io/discord/426318927220441089)

Automations introduced by the Tool:
1.  ENAV AIP ENR 4.4.1 - Enroute significant points description. Exports and parse of the PDF file to an Aurora Readable file, ready for Sector File import.

### Components
1.  EnavData Libary: wrote to manage the export procedure from PDF files to an IVAO readable format. Actually are only implemented strategies for Aurora or Plain Text (debug pourposes) export. Extensible from outside of any kind of export format.
2.  Application: Container and UI for exposing controls to the user, in order tu run conversions made by the library.

### Requirements
Built on the .NET 6 stack. No framework install needed to run.
Actually as Win x64 exe file. Mac and Linux ready.

