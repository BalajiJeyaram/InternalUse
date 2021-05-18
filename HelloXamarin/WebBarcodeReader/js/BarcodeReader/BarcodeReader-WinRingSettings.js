/**
* BarcodeReader-WinRingSettings.js
* @file This file defines the mappings between the scanning API symbology
* settings and Honeywell ring scanner decoder settings. It is dynamically
* loaded depending on the running platform and scanner type. It is used for
* the following platforms and scanners:
* - Dolphin D75e Win10 IoT Mobile with ring scanner
*
* @version 1.00.00.0
*/
var HowneywellBarcodeReaderRingSettings =
[
    {
        "family" : "Symbology",
        "key" : "AustralianPost",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "reverseValueMap" : [{1 : "true"}, {"*" : "false"}],
        "command" : "POSTAL"
    },
    {
        "family" : "Symbology",
        "key" : "Aztec",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "AZTENA"
    },
    {
        "family" : "Symbology",
        "key" : "BPO",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 7}, {"false" : 0}],
        "reverseValueMap" : [{7 : "true"}, {"*" : "false"}],
        "command" : "POSTAL"
    },
    {
        "family" : "Symbology",
        "key" : "CanadaPost",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 30}, {"false" : 0}],
        "reverseValueMap" : [{30 : "true"}, {"*" : "false"}],
        "command" : "POSTAL"
    },
    {
        "family" : "Symbology",
        "key" : "Codabar",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "CBRENA"
    },
    {
        "family" : "Symbology",
        "key" : "CodablockA",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "CBAENA"
    },
    {
        "family" : "Symbology",
        "key" : "CodablockF",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "CBFENA"
    },
    {
        "family" : "Symbology",
        "key" : "Code11",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "C11ENA"
    },
    {
        "family" : "Symbology",
        "key" : "Code39",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "C39ENA"
    },
    {
        "family" : "Symbology",
        "key" : "Code39",
        "option" : "EnableTriopticCode39",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "TRIENA"
    },
    {
        "family" : "Symbology",
        "key" : "Code93",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "C93ENA"
    },
    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "EnableCode128",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "128ENA"
    },
    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "EnableGS1_128",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "GS1ENA"
    },
    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "EnableISBT_128",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "ISBENA"
    },
    {
        "family" : "Symbology",
        "key" : "DataMatrix",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "IDMENA"
    },
    {
        "family" : "Symbology",
        "key" : "DutchPost",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 4}, {"false" : 0}],
        "reverseValueMap" : [{4 : "true"}, {"*" : "false"}],
        "command" : "POSTAL"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableUPCA",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "UPAENA"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableUPCE",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "UPEEN0"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableEAN8",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "EA8ENA"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableEAN13",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "E13ENA"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableUPC_E1",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "UPEEN1"
    },
    {
        "family" : "Symbology",
        "key" : "GS1DataBarExpanded",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "RSEENA"
    },
    {
        "family" : "Symbology",
        "key" : "GS1DataBarLimited",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "RSLENA"
    },
    {
        "family" : "Symbology",
        "key" : "GS1DataBarOmniDirectional",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "RSSENA"
    },
    {
        "family" : "Symbology",
        "key" : "Infomail",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 2}, {"false" : 0}],
        "reverseValueMap" : [{2 : "true"}, {"*" : "false"}],
        "command" : "POSTAL"
    },
    {
        "family" : "Symbology",
        "key" : "IntelligentMail",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 10}, {"false" : 0}],
        "reverseValueMap" : [{10 : "true"}, {"*" : "false"}],
        "command" : "POSTAL"
    },
    {
        "family" : "Symbology",
        "key" : "Interleaved2Of5",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "I25ENA"
    },
    {
        "family" : "Symbology",
        "key" : "JapanPost",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 3}, {"false" : 0}],
        "reverseValueMap" : [{3 : "true"}, {"*" : "false"}],
        "command" : "POSTAL"
    },
    {
        "family" : "Symbology",
        "key" : "Matrix2Of5",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "X25ENA"
    },
    {
        "family" : "Symbology",
        "key" : "Maxicode",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "MAXENA"
    },
    {
        "family" : "Symbology",
        "key" : "MicroPDF417",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "MPDENA"
    },
    {
        "family" : "Symbology",
        "key" : "MSI",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "MSIENA"
    },
    {
        "family" : "Symbology",
        "key" : "PDF417",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "PDFENA"
    },
    {
        "family" : "Symbology",
        "key" : "Planet",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 5}, {"false" : 0}],
        "reverseValueMap" : [{5 : "true"}, {"*" : "false"}],
        "command" : "POSTAL"
    },
    {
        "family" : "Symbology",
        "key" : "Postnet",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 6}, {"false" : 0}],
        "reverseValueMap" : [{6 : "true"}, {"*" : "false"}],
        "command" : "POSTAL"
    },
    {
        "family" : "Symbology",
        "key" : "QRCode",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "QRCENA"
    },
    {
        "family" : "Symbology",
        "key" : "Standard2Of5",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "R25ENA"
    },
    {
        "family" : "Symbology",
        "key" : "Telepen",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "TELENA"
    },
    {
        "family" : "Symbology",
        "key" : "TLC39",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : 1}, {"false" : 0}],
        "command" : "T39ENA"
    },
];