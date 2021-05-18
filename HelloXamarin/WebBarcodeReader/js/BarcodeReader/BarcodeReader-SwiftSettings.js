/**
* BarcodeReader-SwiftSettings.js
* @file This file defines the mappings between the scanning API symbology
* settings and Honeywell swift decoder settings. It is dynamically loaded
* depending on the running platform and scanner type. It is used for the
* following platforms and scanners:
* - Dolphin CT50 Android with internal scanner
* - Dolphin D75e Android with internal scanner
* - Dolphin D75e Android with ring scanner
* - CN51 Android 6.0 with internal scanner
* - Dolphin CT50 Win10 IoT Mobile with internal scanner
* - Dolphin D75e Win10 IoT Mobile with internal scanner
*
* @version 1.00.00.0
*/
var HowneywellBarcodeReaderSwiftSettings =
[
    {
        "family" : "Symbology",
        "key" : "AustralianPost",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "australia"}, {"false" : "none"}],
        "reverseValueMap" : [{"australia" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Aztec",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_AZTEC_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "BPO",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "bpo"}, {"false" : "none"}],
        "reverseValueMap" : [{"bpo" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "CanadaPost",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "canada"}, {"false" : "none"}],
        "reverseValueMap" : [{"canada" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "ChinaPost",
        "option": "Enable",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_HK25_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Codabar",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_CODABAR_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "CodablockA",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_CODABLOCK_A_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "CodablockF",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_CODABLOCK_F_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Code11",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_CODE11_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Code39",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_CODE39_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Code39",
        "option" : "EnableTriopticCode39",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_TRIOPTIC_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Code93",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_CODE93_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "EnableCode128",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_CODE128_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "EnableGS1_128",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_GS1_128_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "EnableISBT_128",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_C128_ISBT_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "DataMatrix",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_DATAMATRIX_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "Digimarc",
        "option": "Enable",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_DIGIMARC_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "Dotcode",
        "option": "Enable",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_DOTCODE_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "DutchPost",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "dutch"}, {"false" : "none"}],
        "reverseValueMap" : [{"dutch" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableUPCA",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_UPCA_ENABLE"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableUPCE",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_UPCE0_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableEAN8",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_EAN8_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableEAN13",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_EAN13_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EnableUPC_E1",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_UPCE1_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "GridMatrix",
        "option": "Enable",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_GRIDMATRIX_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "GS1Composite",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_COMPOSITE_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "GS1DataBarExpanded",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_RSS_EXPANDED_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "GS1DataBarLimited",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_RSS_LIMITED_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "GS1DataBarOmniDirectional",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_RSS_14_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "Hanxin",
        "option": "Enable",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_HANXIN_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "Iata25",
        "option": "Enable",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_IATA25_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Infomail",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "infomail"}, {"false" : "none"}],
        "reverseValueMap" : [{"infomail" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "IntelligentMail",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "usps"}, {"false" : "none"}],
        "reverseValueMap" : [{"usps" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Interleaved2Of5",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_I25_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "JapanPost",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "japan"}, {"false" : "none"}],
        "reverseValueMap" : [{"japan" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "Koreapost",
        "option": "Enable",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_KOREA_POST_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Matrix2Of5",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_M25_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Maxicode",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_MAXICODE_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "MicroPDF417",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_MICROPDF_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "MSI",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_MSI_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "PDF417",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_PDF417_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Planet",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "planet"}, {"false" : "none"}],
        "reverseValueMap" : [{"planet" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Postnet",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "postnet"}, {"false" : "none"}],
        "reverseValueMap" : [{"postnet" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "QRCode",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_QR_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Standard2Of5",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_S25_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "SwedenPost",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : "sweden"}, {"false" : "none"}],
        "reverseValueMap" : [{"sweden" : "true"}, {"*" : "false"}],
        "command" : "DEC_POSTAL_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Telepen",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_TELEPEN_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "TLC39",
        "option" : "Enable",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_TLC39_ENABLED"
    },


    {
        "family" : "Symbology",
        "key" : "Aztec",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 3852}],
        "command" : "DEC_AZTEC_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Aztec",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 3852}],
        "command" : "DEC_AZTEC_MAX_LENGTH"
    },

    {
        "family": "Symbology",
        "key": "ChinaPost",
        "option": "MinLen",
        "valueType": "int",
        "valueRange": [{ "min": 4 }, { "max": 80}],
        "command": "DEC_HK25_MIN_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "ChinaPost",
        "option": "MaxLen",
        "valueType": "int",
        "valueRange": [{ "min": 4 }, { "max": 80}],
        "command": "DEC_HK25_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "Codabar",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_CODABAR_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Codabar",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_CODABAR_MAX_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Codabar",
        "option" : "CheckDigitMode",
        "valueType" : "list",
        "values" : ["noCheck", "check", "checkAndStrip"],
        "command" : "DEC_CODABAR_CHECK_DIGIT_MODE"
    },
    {
        "family": "Symbology",
        "key": "Codabar",
        "option": "CodabarConcatEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_CODABAR_CONCAT_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Codabar",
        "option" : "StartStopTransmit",
        "valueType" : "map",
        "valueMap" : [{"A,B,C,D" : true}, {"Disable" : false}],
        "command" : "DEC_CODABAR_START_STOP_TRANSMIT"
    },

    {
        "family" : "Symbology",
        "key" : "CodablockA",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 2048}],
        "command" : "DEC_CODABLOCK_A_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "CodablockA",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 2048}],
        "command" : "DEC_CODABLOCK_A_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "CodablockF",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 2048}],
        "command" : "DEC_CODABLOCK_F_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "CodablockF",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 2048}],
        "command" : "DEC_CODABLOCK_F_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "Code11",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_CODE11_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Code11",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_CODE11_MAX_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Code11",
        "option" : "CheckDigitMode",
        "valueType" : "list",
        "values" : ["doubleDigitCheck", "singleDigitCheck",
                    "doubleDigitCheckAndStrip", "singleDigitCheckAndStrip"],
        "command" : "DEC_CODE11_CHECK_DIGIT_MODE"
    },

    {
        "family" : "Symbology",
        "key" : "Code39",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_CODE39_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Code39",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_CODE39_MAX_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Code39",
        "option" : "CheckDigitMode",
        "valueType" : "list",
        "values" : ["noCheck", "check", "checkAndStrip"],
        "command" : "DEC_CODE39_CHECK_DIGIT_MODE"
    },
    {
        "family" : "Symbology",
        "key" : "Code39",
        "option" : "FullAsciiEnabled",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_CODE39_FULL_ASCII_ENABLED"
    },
    {
        "family" : "Symbology",
        "key" : "Code39",
        "option" : "StartStopTransmission",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_CODE39_START_STOP_TRANSMIT"
    },
    {
        "family": "Symbology",
        "key": "Code39",
        "option": "Base32Enabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_CODE39_BASE32_ENABLED"
    },

    {
        "family" : "Symbology",
        "key" : "Code93",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 80}],
        "command" : "DEC_CODE93_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Code93",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 80}],
        "command" : "DEC_CODE93_MAX_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "Code93",
        "option": "Code93HighDensity",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_CODE93_HIGH_DENSITY"
    },

    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 80}],
        "command" : "DEC_CODE128_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 80}],
        "command" : "DEC_CODE128_MAX_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "Code128",
        "option": "ShortMargin",
        "valueType": "list",
        "values": ["disabled", "partial", "full"],
        "command": "DEC_C128_SHORT_MARGIN"
    },
    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "GS1_128_MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 80}],
        "command" : "DEC_GS1_128_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Code128",
        "option" : "GS1_128_MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 80}],
        "command" : "DEC_GS1_128_MAX_LENGTH"
    },

    {
        "family": "Symbology",
        "key": "GS1Composite",
        "option": "MinLen",
        "valueType": "int",
        "valueRange": [{ "min": 1 }, { "max": 300}],
        "command": "DEC_COMPOSITE_MIN_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "GS1Composite",
        "option": "MaxLen",
        "valueType": "int",
        "valueRange": [{ "min": 1 }, { "max": 300}],
        "command": "DEC_COMPOSITE_MAX_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "GS1Composite",
        "option": "CompositeWithUPCEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_COMPOSITE_WITH_UPC_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "GS1Composite",
        "option": "CombineComposites",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_COMBINE_COMPOSITES"
    },

    {
        "family" : "Symbology",
        "key" : "DataMatrix",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 3166}],
        "command" : "DEC_DATAMATRIX_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "DataMatrix",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 3166}],
        "command" : "DEC_DATAMATRIX_MAX_LENGTH"
    },

    {
        "family": "Symbology",
        "key": "Digimarc",
        "option": "DigimarcConversion",
        "valueType": "list",
        "values": ["noConversion", "convertToGtinEquivalent"],
        "command": "DEC_DIGIMARC_CONVERSION"
    },
    {
        "family": "Symbology",
        "key": "Digimarc",
        "option": "DigimarcScaleBlocks",
        "valueType": "list",
        "values": ["useBothScale1AndScale3Blocks", "useScale1Blocks", "useScale3Blocks"],
        "command": "DEC_DIGIMARC_SCALE_BLOCKS"
    },
    {
        "family": "Symbology",
        "key": "Digimarc",
        "option": "DigimarcShapeDetection",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_DIGIMARC_SHAPE_DETECTION"
    },

    {
        "family": "Symbology",
        "key": "Dotcode",
        "option": "MinLen",
        "valueType": "int",
        "valueRange": [{ "min": 1 }, { "max": 2400}],
        "command": "DEC_DOTCODE_MIN_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "Dotcode",
        "option": "MaxLen",
        "valueType": "int",
        "valueRange": [{ "min": 1 }, { "max": 2400}],
        "command": "DEC_DOTCODE_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "UPCACheckDigit",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_UPCA_CHECK_DIGIT_TRANSMIT"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "TranslateToEAN13",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCA_TRANSLATE_TO_EAN13"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "CouponCode",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_COUPON_CODE_MODE"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "CombineCouponCodes",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_COMBINE_COUPON_CODES"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCANumberSystemTransmit",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCA_NUMBER_SYSTEM_TRANSMIT"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCA2CharAddendaEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCA_2CHAR_ADDENDA_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCA5CharAddendaEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCA_5CHAR_ADDENDA_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCAAddendaRequired",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCA_ADDENDA_REQUIRED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCAAddendaSeparator",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCA_ADDENDA_SEPARATOR"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "UPCECheckDigit",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_UPCE_CHECK_DIGIT_TRANSMIT"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EAN8CheckDigit",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_EAN8_CHECK_DIGIT_TRANSMIT"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "EAN8_2CharAddendaEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_EAN8_2CHAR_ADDENDA_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "EAN8_5CharAddendaEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_EAN8_5CHAR_ADDENDA_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "EAN8AddendaRequired",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_EAN8_ADDENDA_REQUIRED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "EAN8AddendaSeparator",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_EAN8_ADDENDA_SEPARATOR"
    },
    {
        "family" : "Symbology",
        "key" : "EANUPC",
        "option" : "EAN13CheckDigit",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_EAN13_CHECK_DIGIT_TRANSMIT"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "EAN13_2CharAddendaEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_EAN13_2CHAR_ADDENDA_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "EAN13_5CharAddendaEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_EAN13_5CHAR_ADDENDA_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "EAN13AddendaRequired",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_EAN13_ADDENDA_REQUIRED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "EAN13AddendaSeparator",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_EAN13_ADDENDA_SEPARATOR"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "ExpandToUPCA",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCE_EXPAND"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCENumberSystemTransmit",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCE_NUMBER_SYSTEM_TRANSMIT"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCE2CharAddendaEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCE_2CHAR_ADDENDA_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCE5CharAddendaEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCE_5CHAR_ADDENDA_ENABLED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCEAddendaRequired",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCE_ADDENDA_REQUIRED"
    },
    {
        "family": "Symbology",
        "key": "EANUPC",
        "option": "UPCEAddendaSeparator",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_UPCE_ADDENDA_SEPARATOR"
    },

    {
        "family": "Symbology",
        "key": "GridMatrix",
        "option": "MinLen",
        "valueType": "int",
        "valueRange": [{ "min": 1 }, { "max": 2751}],
        "command": "DEC_GRIDMATRIX_MIN_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "GridMatrix",
        "option": "MaxLen",
        "valueType": "int",
        "valueRange": [{ "min": 1 }, { "max": 2751}],
        "command": "DEC_GRIDMATRIX_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "GS1DataBarExpanded",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 80}],
        "command" : "DEC_RSS_EXPANDED_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "GS1DataBarExpanded",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 80}],
        "command" : "DEC_RSS_EXPANDED_MAX_LENGTH"
    },

    {
        "family": "Symbology",
        "key": "Hanxin",
        "option": "MinLen",
        "valueType": "int",
        "valueRange": [{ "min": 1 }, { "max": 6000}],
        "command": "DEC_HANXIN_MIN_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "Hanxin",
        "option": "MaxLen",
        "valueType": "int",
        "valueRange": [{ "min": 1 }, { "max": 6000}],
        "command": "DEC_HANXIN_MAX_LENGTH"
    },

    {
        "family": "Symbology",
        "key": "Iata25",
        "option": "MinLen",
        "valueType": "int",
        "valueRange": [{ "min": 4 }, { "max": 80}],
        "command": "DEC_IATA25_MIN_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "Iata25",
        "option": "MaxLen",
        "valueType": "int",
        "valueRange": [{ "min": 4 }, { "max": 80}],
        "command": "DEC_IATA25_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "Interleaved2Of5",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_I25_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Interleaved2Of5",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_I25_MAX_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Interleaved2Of5",
        "option" : "CheckDigitMode",
        "valueType" : "list",
        "values" : ["noCheck", "check", "checkAndStrip"],
        "command" : "DEC_I25_CHECK_DIGIT_MODE"
    },

    {
        "family": "Symbology",
        "key": "Koreapost",
        "option": "MinLen",
        "valueType": "int",
        "valueRange": [{ "min": 4 }, { "max": 48}],
        "command": "DEC_KOREA_POST_MIN_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "Koreapost",
        "option": "MaxLen",
        "valueType": "int",
        "valueRange": [{ "min": 4 }, { "max": 48}],
        "command": "DEC_KOREA_POST_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "Matrix2Of5",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 4}, {"max" : 80}],
        "command" : "DEC_M25_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Matrix2Of5",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 4}, {"max" : 80}],
        "command" : "DEC_M25_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "Maxicode",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 150}],
        "command" : "DEC_MAXICODE_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Maxicode",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 150}],
        "command" : "DEC_MAXICODE_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "MicroPDF417",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 2750}],
        "command" : "DEC_MICROPDF_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "MicroPDF417",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 2750}],
        "command" : "DEC_MICROPDF_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "MSI",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_MSI_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "MSI",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 0}, {"max" : 48}],
        "command" : "DEC_MSI_MAX_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "MSI",
        "option" : "CheckDigitMode",
        "valueType" : "list",
        "values" : ["noCheck", "singleMod10Check", "singleMod10PlusMod10Check",
                    "doubleMod10Check", "singleMod10CheckAndStrip", "doubleMod10CheckAndStrip"],
        "command" : "DEC_MSI_CHECK_DIGIT_MODE"
    },
    {
        "family": "Symbology",
        "key": "MSI",
        "option": "ShortMargin",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_MSIP_SHORT_MARGIN"
    },
    {
        "family": "Symbology",
        "key": "MSI",
        "option": "OutOfSpecSymbol",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_PROP_MSIP_OUT_OF_SPEC_SYMBOL"
    },

    {
        "family" : "Symbology",
        "key" : "PDF417",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 2750}],
        "command" : "DEC_PDF417_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "PDF417",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 2750}],
        "command" : "DEC_PDF417_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "Planet",
        "option" : "CheckDigitTransmit",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_PLANETCODE_CHECK_DIGIT_TRANSMIT"
    },
    
    {
        "family" : "Symbology",
        "key" : "Postnet",
        "option" : "CheckDigitTransmit",
        "valueType" : "map",
        "valueMap" : [{"true" : true}, {"false" : false}],
        "command" : "DEC_POSTNET_CHECK_DIGIT_TRANSMIT"
    },

    {
        "family" : "Symbology",
        "key" : "QRCode",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 7089}],
        "command" : "DEC_QR_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "QRCode",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 7089}],
        "command" : "DEC_QR_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "Standard2Of5",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 4}, {"max" : 48}],
        "command" : "DEC_S25_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Standard2Of5",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 4}, {"max" : 48}],
        "command" : "DEC_S25_MAX_LENGTH"
    },

    {
        "family" : "Symbology",
        "key" : "Telepen",
        "option" : "MinLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 60}],
        "command" : "DEC_TELEPEN_MIN_LENGTH"
    },
    {
        "family" : "Symbology",
        "key" : "Telepen",
        "option" : "MaxLen",
        "valueType" : "int",
        "valueRange" : [{"min" : 1}, {"max" : 60}],
        "command" : "DEC_TELEPEN_MAX_LENGTH"
    },
    {
        "family": "Symbology",
        "key": "Telepen",
        "option": "OldStyle",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "DEC_TELEPEN_OLD_STYLE"
    },

    {
        "family" : "BarcodeReader",
        "key" : "Settings",
        "option" : "TriggerMode",
        "valueType" : "list",
        "values" : ["continuous", "oneShot", "readOnRelease", "readOnSecondTriggerPress"],
        "command" : "TRIG_SCAN_MODE"
    },
    {
        "family": "BarcodeReader",
        "key": "Settings",
        "option": "SameSymbolTimeoutEnabled",
        "valueType": "map",
        "valueMap": [{ "true": true }, { "false": false}],
        "command": "TRIG_SCAN_SAME_SYMBOL_TIMEOUT_ENABLED"
    },
    {
        "family": "BarcodeReader",
        "key": "Settings",
        "option": "SameSymbolTimeout",
        "valueType": "int",
        "valueRange": [1000],
        "command": "TRIG_SCAN_SAME_SYMBOL_TIMEOUT"
    },
    {
        "family": "BarcodeReader",
        "key": "Settings",
        "option": "Prefix",
        "valueType": "string",
        "value":[""],
        "command": "DPR_PREFIX"
    },
    {
        "family": "BarcodeReader",
        "key": "Settings",
        "option": "Suffix",
        "valueType": "string",
        "value": [""],
        "command": "DPR_SUFFIX"
        },
        {
            "family": "DecodeSecurity",
            "key": "",
            "option": "CenterDecoding",
            "valueType": "string",
            "value": [""],
            "command": "DPR_SUFFIX"
        }

];
