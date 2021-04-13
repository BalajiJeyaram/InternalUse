(function () {
    HoneywellBarcodeReaderAjax = function (a, c) {
        var e = this; if (!a || HoneywellBarcodeReaderUtils.stdParamCheck(a, "scannerName", "string", c)) {
            var f = { method: "scanner.connect" }; a && (f.params = {}, f.params.scanner = a); HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", f, function (a) {
                if (HoneywellBarcodeReaderUtils.hasProperty(a, "result", !0)) if (HoneywellBarcodeReaderUtils.hasProperty(a.result, "session", !0)) {
                    e.sessionId = a.result.session; var k = { method: "scanner.claim", params: {} }; k.params.session =
                        e.sessionId; HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", k, function (a) {
                            HoneywellBarcodeReaderUtils.hasProperty(a, "result", !0) ? HoneywellBarcodeReaderUtils.hasProperty(a.result, "filter", !0) ? (e.filter = a.result.filter, HoneywellBarcodeReaderUtils.isFunction(c) && setTimeout(function () { c({ status: 0, message: HoneywellBarcodeReaderUtils.MSG_OPERATION_COMPLETED }) }, 0)) : HoneywellBarcodeReaderUtils.isFunction(c) && setTimeout(function () {
                                c({
                                    status: HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR,
                                    message: "JSON-RPC parsing error in response, missing filter parameter."
                                })
                            }, 0) : HoneywellBarcodeReaderUtils.hasJsonRpcError(a) ? HoneywellBarcodeReaderUtils.isFunction(c) && setTimeout(function () { c({ status: a.error.code, message: a.error.message }) }, 0) : HoneywellBarcodeReaderUtils.isFunction(c) && setTimeout(function () { c({ status: HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, message: "JSON-RPC parsing error in response." }) }, 0)
                        })
                } else HoneywellBarcodeReaderUtils.isFunction(c) && setTimeout(function () {
                    c({
                        status: HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR,
                        message: "JSON-RPC parsing error in response, missing session parameter."
                    })
                }, 0); else HoneywellBarcodeReaderUtils.hasJsonRpcError(a) ? HoneywellBarcodeReaderUtils.isFunction(c) && setTimeout(function () { c({ status: a.error.code, message: a.error.message }) }, 0) : HoneywellBarcodeReaderUtils.isFunction(c) && setTimeout(function () { c({ status: HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, message: "JSON-RPC parsing error in response." }) }, 0)
            })
        }
    }; HoneywellBarcodeReaderAjax.prototype = {
        version: "1.20.00.0825", sessionId: null,
        filter: null, eventDispatcher: null, barcodeDataReadyListeners: [], batchGetBuffer: [], batchSetBuffer: [], activate: function (a, c) {
            var e = this; if (this.verifyActiveConnection(c) && HoneywellBarcodeReaderUtils.stdParamCheck(a, "on", "boolean", c)) if (a) {
                var f = { method: "internal.setTrigger", params: {} }; f.params.session = e.sessionId; f.params.state = !1; HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", f, function (a) {
                    if (HoneywellBarcodeReaderUtils.hasProperty(a, "result", !1)) {
                        var k = {
                            method: "internal.setTrigger",
                            params: {}
                        }; k.params.session = e.sessionId; k.params.state = !0; HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", k, function (a) { HoneywellBarcodeReaderUtils.stdErrorCheck(a, c) })
                    } else HoneywellBarcodeReaderUtils.hasJsonRpcError(a) ? HoneywellBarcodeReaderUtils.isFunction(c) && setTimeout(function () { c({ status: a.error.code, message: a.error.message }) }, 0) : HoneywellBarcodeReaderUtils.isFunction(c) && setTimeout(function () { c({ status: HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, message: "JSON-RPC parsing error in response." }) },
                        0)
                })
            } else f = { method: "internal.setTrigger", params: {} }, f.params.session = e.sessionId, f.params.state = !1, HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", f, function (a) { HoneywellBarcodeReaderUtils.stdErrorCheck(a, c) })
        }, addEventListener: function (a, c, e) {
            function f(a, b, c) {
                if ("scanner.barcodeEvent" === a && b && c) {
                    var e = a = null, f = null, l, n; if (HoneywellBarcodeReaderUtils.hasProperty(b, "barcode", !0) && (HoneywellBarcodeReaderUtils.hasProperty(b.barcode, "data", !0) && (a = b.barcode.data), HoneywellBarcodeReaderUtils.hasProperty(b.barcode,
                        "honeywellId", !0) && (l = b.barcode.honeywellId), HoneywellBarcodeReaderUtils.hasProperty(b.barcode, "aimId", !0) && (n = b.barcode.aimId), e = HoneywellBarcodeReaderUtils.getSymbologyName(l, n), HoneywellBarcodeReaderUtils.hasProperty(b.barcode, "timestamp", !0) && (f = b.barcode.timestamp), d.barcodeDataReadyListeners[c] instanceof Array)) for (b = d.barcodeDataReadyListeners[c], c = 0, l = b.length; c < l; c++)b[c](a, e, f)
                }
            } var d = this; d.filter && (e = { error: null, event: f, filter: d.filter }, "barcodedataready" === a && (d.eventDispatcher || (d.eventDispatcher =
                HoneywellBarcodeReaderWebEventDispatcher.create("datacollection"), d.eventDispatcher.startSession(e)), "undefined" === typeof d.barcodeDataReadyListeners[d.filter] && (d.barcodeDataReadyListeners[d.filter] = []), d.barcodeDataReadyListeners[d.filter].push(c)))
        }, clearBuffer: function () { this.batchGetBuffer.length = 0; this.batchSetBuffer.length = 0 }, close: function (a) {
            var c = this; if (null === this.sessionId) HoneywellBarcodeReaderUtils.isFunction(a) && setTimeout(function () { a({ status: 0, message: "BarcodeReader already closed" }) },
                0); else {
                    var e = { method: "scanner.release", params: {} }; e.params.session = c.sessionId; HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", e, function (e) {
                        HoneywellBarcodeReaderUtils.stdErrorCheck(e, function (d) {
                            if (0 === d.status) {
                                c.filter = null; var e = { method: "scanner.disconnect", params: {} }; e.params.session = c.sessionId; HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", e, function (b) {
                                    HoneywellBarcodeReaderUtils.stdErrorCheck(b, function (b) {
                                        0 === b.status && (c.sessionId = null, c.eventDispatcher &&
                                            c.eventDispatcher.stopSession()); HoneywellBarcodeReaderUtils.isFunction(a) && setTimeout(function () { a(b) }, 0)
                                    })
                                })
                            } else HoneywellBarcodeReaderUtils.isFunction(a) && setTimeout(function () { a(d) }, 0)
                        })
                    })
            }
        }, commitBuffer: function (a) {
            function c() {
                if (0 < d.length) {
                    var b = { method: "scanner.getProperties", params: {} }; b.params.session = e.sessionId; b.params.names = []; for (var c = 0, f = d.length; c < f; c++)b.params.names.push(d[c].command); e.logVar("Batch get request.params.names", b.params.names, !1); HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection",
                        b, function (b) {
                            e.logVar("Batch get's response", b, !1); if (HoneywellBarcodeReaderUtils.isFunction(a)) {
                                if (HoneywellBarcodeReaderUtils.hasProperty(b, "result", !0)) if (HoneywellBarcodeReaderUtils.hasProperty(b.result, "values", !0)) for (var c = 0, f = d.length; c < f; c++) {
                                    var g = d[c], h = { method: "getBuffered" }; h.family = g.family; h.key = g.key; h.option = g.option; HoneywellBarcodeReaderUtils.hasProperty(b.result.values, g.command, !0) ? HoneywellBarcodeReaderUtils.convertScannerSettingValue(g, b.result.values[g.command], h) ? (h.status =
                                        0, h.message = HoneywellBarcodeReaderUtils.MSG_OPERATION_COMPLETED) : (h.status = HoneywellBarcodeReaderErrors.INVALID_SETTING_VALUE, h.message = "Unexpected scanner setting value") : (h.status = HoneywellBarcodeReaderErrors.INVALID_PARAMETER, h.message = "Invalid scanner property: " + g.command); k.push(h)
                                } else h = { method: "getBuffered", family: null, key: null, option: null }, h.status = HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, h.message = "JSON-RPC parsing error in response.", k.push(h); else HoneywellBarcodeReaderUtils.hasJsonRpcError(b) ?
                                    (h = { method: "getBuffered", family: null, key: null, option: null }, h.status = b.error.code, h.message = b.error.message) : (h = { method: "getBuffered", family: null, key: null, option: null }, h.status = HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, h.message = "JSON-RPC parsing error in response."), k.push(h); setTimeout(function () { a(k) }, 0)
                            }
                        })
                } else HoneywellBarcodeReaderUtils.isFunction(a) && setTimeout(function () { a(k) }, 0)
            } var e = this, f = [], d = [], k = []; if (null === this.sessionId || null === this.filter) {
                if (HoneywellBarcodeReaderUtils.isFunction(a)) {
                    var b =
                        {}; b.status = HoneywellBarcodeReaderErrors.NO_CONNECTION; b.message = "No scanner connection"; b.method = null; b.family = null; b.key = null; b.option = null; k.push(b); setTimeout(function () { a(k) }, 0)
                }
            } else if (0 === this.batchSetBuffer.length && 0 === this.batchGetBuffer.length) HoneywellBarcodeReaderUtils.isFunction(a) && (b = {}, b.status = HoneywellBarcodeReaderErrors.EMPTY_COMMIT_BUFFER, b.message = "The commit buffer is empty, nothing to commit.", b.method = null, b.family = null, b.key = null, b.option = null, k.push(b), setTimeout(function () { a(k) },
                0)); else {
                    for (var g = 0, m = e.batchSetBuffer.length; g < m; g++) { var h = e.batchSetBuffer[g]; 0 === h.status ? f.push(h) : (b = { method: "setBuffered" }, b.family = h.family, b.key = h.key, b.option = h.option, b.status = h.status, b.message = h.message, k.push(b)) } g = 0; for (m = e.batchGetBuffer.length; g < m; g++)h = e.batchGetBuffer[g], 0 === h.status ? d.push(h) : (b = { method: "getBuffered" }, b.family = h.family, b.key = h.key, b.option = h.option, b.status = h.status, b.message = h.message, k.push(b)); if (0 < f.length) {
                        var l = [], b = { method: "scanner.setProperties", params: {} };
                        b.params.session = e.sessionId; b.params.values = {}; g = 0; for (m = f.length; g < m; g++)h = f[g], b.params.values[h.command] = h.value, l.push(h.command); e.logVar("Batch set request.params.values", b.params.values, !1); HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", b, function (b) {
                            if (HoneywellBarcodeReaderUtils.isFunction(a)) if (HoneywellBarcodeReaderUtils.hasProperty(b, "result", !1)) b = { method: "scanner.getProperties", params: {} }, b.params.session = e.sessionId, b.params.names = l, HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection",
                                b, function (a) {
                                    e.logVar("Batch set's get response", a, !1); if (HoneywellBarcodeReaderUtils.hasProperty(a, "result", !0) && HoneywellBarcodeReaderUtils.hasProperty(a.result, "values", !0)) for (var b = 0, d = f.length; b < d; b++) {
                                        var g = f[b], h = { method: "setBuffered" }; h.family = g.family; h.key = g.key; h.option = g.option; HoneywellBarcodeReaderUtils.hasProperty(a.result.values, g.command, !0) ? a.result.values[g.command] === g.value ? (h.status = 0, h.message = HoneywellBarcodeReaderUtils.MSG_OPERATION_COMPLETED) : (h.status = HoneywellBarcodeReaderErrors.INVALID_SETTING_VALUE,
                                            h.message = "Scanner rejects the setting value.") : (h.status = HoneywellBarcodeReaderErrors.INVALID_PARAMETER, h.message = "Invalid scanner property: " + g.command); k.push(h)
                                    } else HoneywellBarcodeReaderUtils.hasJsonRpcError(a) ? (h = { method: "setBuffered", family: null, key: null, option: null }, h.status = a.error.code, h.message = a.error.message) : (h = { method: "setBuffered", family: null, key: null, option: null }, h.status = HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, h.message = "JSON-RPC parsing error in response."), k.push(h); c()
                                });
                            else { if (HoneywellBarcodeReaderUtils.hasJsonRpcError(b)) { var d = { method: "setBuffered", family: null, key: null, option: null }; d.status = b.error.code; d.message = b.error.message } else d = { method: "setBuffered", family: null, key: null, option: null }, d.status = HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, d.message = "JSON-RPC parsing error in response."; k.push(d); c() }
                        })
                    } else c()
            }
        }, enableTrigger: function (a, c) {
            if (this.verifyActiveConnection(c) && HoneywellBarcodeReaderUtils.stdParamCheck(a, "enabled", "boolean", c)) {
                var e = {
                    method: "scanner.setProperties",
                    params: {}
                }; e.params.session = this.sessionId; e.params.values = {}; e.params.values.TRIG_CONTROL_MODE = a ? "autoControl" : "disable"; HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", e, function (a) { HoneywellBarcodeReaderUtils.stdErrorCheck(a, c) })
            }
        }, get: function (a, c, e, f) {
            var d = this, k, b; b = {}; b.family = a; b.key = c; b.option = e; HoneywellBarcodeReaderUtils.isFunction(f) && ("undefined" === typeof HowneywellBarcodeReaderSwiftSettings ? (b.status = HoneywellBarcodeReaderErrors.MISSING_SETTINGS_DEF, b.message =
                "Missing settings definition HowneywellBarcodeReaderSwiftSettings.", setTimeout(function () { f(b) }, 0)) : null === this.sessionId || null === this.filter ? (b.status = HoneywellBarcodeReaderErrors.NO_CONNECTION, b.message = "No scanner connection", setTimeout(function () { f(b) }, 0)) : HoneywellBarcodeReaderUtils.stdParamCheckResult(a, "family", "string", b, f) && (HoneywellBarcodeReaderUtils.stdParamCheckResult(c, "key", "string", b, f) && HoneywellBarcodeReaderUtils.stdParamCheckResult(e, "option", "string", b, f)) && (k = HoneywellBarcodeReaderUtils.getSettingDef(HowneywellBarcodeReaderSwiftSettings,
                    a, c, e, null, !1), 0 === k.status ? (a = { method: "scanner.getProperties", params: {} }, a.params.session = d.sessionId, a.params.names = [], a.params.names.push(k.command), d.logVar("get settingDef", k, !1), HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", a, function (a) {
                        HoneywellBarcodeReaderUtils.hasProperty(a, "result", !0) ? HoneywellBarcodeReaderUtils.hasProperty(a.result, "values", !0) ? (a = a.result.values[k.command], d.logVar("get response scannerSettingValue", a, !0), HoneywellBarcodeReaderUtils.convertScannerSettingValue(k,
                            a, b) ? (b.status = 0, b.message = HoneywellBarcodeReaderUtils.MSG_OPERATION_COMPLETED) : (b.status = HoneywellBarcodeReaderErrors.INVALID_SETTING_VALUE, b.message = "Unexpected scanner setting value"), setTimeout(function () { f(b) }, 0)) : (b.status = HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, b.message = "JSON-RPC parsing error in response.") : (HoneywellBarcodeReaderUtils.hasJsonRpcError(a) ? (b.status = a.error.code, b.message = a.error.message) : (b.status = HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, b.message = "JSON-RPC parsing error in response."),
                                setTimeout(function () { f(b) }, 0))
                    })) : (b.status = k.status, b.message = k.message, setTimeout(function () { f(b) }, 0))))
        }, getBuffered: function (a, c, e, f) {
            var d; d = {}; d.family = a; d.key = c; d.option = e; "undefined" === typeof HowneywellBarcodeReaderSwiftSettings ? HoneywellBarcodeReaderUtils.isFunction(f) && (d.status = HoneywellBarcodeReaderErrors.MISSING_SETTINGS_DEF, d.message = "Missing settings definition HowneywellBarcodeReaderSwiftSettings.", setTimeout(function () { f(d) }, 0)) : null === this.sessionId || null === this.filter ? HoneywellBarcodeReaderUtils.isFunction(f) &&
                (d.status = HoneywellBarcodeReaderErrors.NO_CONNECTION, d.message = "No scanner connection", setTimeout(function () { f(d) }, 0)) : HoneywellBarcodeReaderUtils.stdParamCheckResult(a, "family", "string", d, f) && (HoneywellBarcodeReaderUtils.stdParamCheckResult(c, "key", "string", d, f) && HoneywellBarcodeReaderUtils.stdParamCheckResult(e, "option", "string", d, f)) && (a = HoneywellBarcodeReaderUtils.getSettingDef(HowneywellBarcodeReaderSwiftSettings, a, c, e, null, !1), this.logVar("getBuffered settingDef", a, !1), this.batchGetBuffer.push(a),
                    0 === a.status ? HoneywellBarcodeReaderUtils.isFunction(f) && (d.status = 0, d.message = "Get request successfully buffered.", setTimeout(function () { f(d) }, 0)) : HoneywellBarcodeReaderUtils.isFunction(f) && (d.status = a.status, d.message = a.message, setTimeout(function () { f(d) }, 0)))
        }, getLicenseInfo: function (a) {
            if (HoneywellBarcodeReaderUtils.isFunction(a)) {
                var c = { method: "license.getLicenseInfo", params: {} }; c.params.subsystem = "datacollection"; HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("license", c, function (c) {
                    HoneywellBarcodeReaderUtils.hasProperty(c,
                        "result", !0) ? (c.result.status = 0, c.result.message = "Get license info completed successfully.", setTimeout(function () { a(c.result) }, 0)) : HoneywellBarcodeReaderUtils.hasJsonRpcError(c) ? setTimeout(function () { a({ status: c.error.code, message: c.error.message }) }, 0) : setTimeout(function () { a({ status: HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, message: "JSON-RPC parsing error in response." }) }, 0)
                })
            }
        }, logVar: function (a, c, e) {
            if (typeof HoneywellBarcodeReaderUtils.log === typeof Function) {
                var f = typeof c; "object" === f ? null !==
                    c ? "[object Array]" === Object.prototype.toString.call(c) ? (a = a + "\x3d" + c.toString(), e && (a += ", type\x3dArray")) : (a = a + "\x3d" + JSON.stringify(c, null, " "), e && (a += ", type\x3dobject")) : (a += "\x3dnull", e && (a += ", type\x3dobject")) : "undefined" === f ? (a += "\x3dundefined", e && (a += ", type\x3dundefined")) : (a = a + "\x3d" + c.toString(), e && (a += ", type\x3d" + f)); HoneywellBarcodeReaderUtils.log(a)
            }
        }, removeEventListener: function (a, c) {
            if (this.filter && "barcodedataready" === a && this.barcodeDataReadyListeners[this.filter] instanceof Array) for (var e =
                this.barcodeDataReadyListeners[this.filter], f = 0, d = e.length; f < d; f++)if (e[f] === c) { e.splice(f, 1); break }
        }, set: function (a, c, e, f, d) {
            var k = this, b, g; g = {}; g.family = a; g.key = c; g.option = e; "undefined" === typeof HowneywellBarcodeReaderSwiftSettings ? HoneywellBarcodeReaderUtils.isFunction(d) && (g.status = HoneywellBarcodeReaderErrors.MISSING_SETTINGS_DEF, g.message = "Missing settings definition HowneywellBarcodeReaderSwiftSettings.", setTimeout(function () { d(g) }, 0)) : null === this.sessionId || null === this.filter ? HoneywellBarcodeReaderUtils.isFunction(d) &&
                (g.status = HoneywellBarcodeReaderErrors.NO_CONNECTION, g.message = "No scanner connection", setTimeout(function () { d(g) }, 0)) : HoneywellBarcodeReaderUtils.stdParamCheckResult(a, "family", "string", g, d) && (HoneywellBarcodeReaderUtils.stdParamCheckResult(c, "key", "string", g, d) && HoneywellBarcodeReaderUtils.stdParamCheckResult(e, "option", "string", g, d) && HoneywellBarcodeReaderUtils.stdParamCheckResult(f, "value", "string", g, d)) && (b = HoneywellBarcodeReaderUtils.getSettingDef(HowneywellBarcodeReaderSwiftSettings, a, c, e,
                    f, !0), 0 === b.status ? (a = { method: "scanner.setProperties", params: {} }, a.params.session = k.sessionId, a.params.values = {}, a.params.values[b.command] = b.value, k.logVar("set settingDef", b, !1), k.logVar("set request.params.values", a.params.values, !1), HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", a, function (a) {
                        HoneywellBarcodeReaderUtils.isFunction(d) && (HoneywellBarcodeReaderUtils.hasProperty(a, "result", !1) ? (a = { method: "scanner.getProperties", params: {} }, a.params.session = k.sessionId, a.params.names =
                            [], a.params.names.push(b.command), HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection", a, function (a) {
                                k.logVar("set function get response", a, !1); HoneywellBarcodeReaderUtils.hasProperty(a, "result", !0) && HoneywellBarcodeReaderUtils.hasProperty(a.result, "values", !0) ? HoneywellBarcodeReaderUtils.hasProperty(a.result.values, b.command, !0) ? a.result.values[b.command] === b.value ? (g.status = 0, g.message = HoneywellBarcodeReaderUtils.MSG_OPERATION_COMPLETED) : (g.status = HoneywellBarcodeReaderErrors.INVALID_SETTING_VALUE,
                                    g.message = "Scanner rejects the setting value.") : (g.status = HoneywellBarcodeReaderErrors.INVALID_PARAMETER, g.message = "Invalid scanner property: " + b.command) : HoneywellBarcodeReaderUtils.hasJsonRpcError(a) ? (g.status = a.error.code, g.message = a.error.message) : (g.status = HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR, g.message = "JSON-RPC parsing error in response."); setTimeout(function () { d(g) }, 0)
                            })) : (HoneywellBarcodeReaderUtils.hasJsonRpcError(a) ? (g.status = a.error.code, g.message = a.error.message) : (g.status = HoneywellBarcodeReaderErrors.JSON_PARSE_ERROR,
                                g.message = "JSON-RPC parsing error in response."), setTimeout(function () { d(g) }, 0)))
                    })) : HoneywellBarcodeReaderUtils.isFunction(d) && (g.status = b.status, g.message = b.message, setTimeout(function () { d(g) }, 0)))
        }, setBuffered: function (a, c, e, f, d) {
            var k; k = {}; k.family = a; k.key = c; k.option = e; "undefined" === typeof HowneywellBarcodeReaderSwiftSettings ? HoneywellBarcodeReaderUtils.isFunction(d) && (k.status = HoneywellBarcodeReaderErrors.MISSING_SETTINGS_DEF, k.message = "Missing settings definition HowneywellBarcodeReaderSwiftSettings.",
                setTimeout(function () { d(k) }, 0)) : null === this.sessionId || null === this.filter ? HoneywellBarcodeReaderUtils.isFunction(d) && (k.status = HoneywellBarcodeReaderErrors.NO_CONNECTION, k.message = "No scanner connection", setTimeout(function () { d(k) }, 0)) : HoneywellBarcodeReaderUtils.stdParamCheckResult(a, "family", "string", k, d) && (HoneywellBarcodeReaderUtils.stdParamCheckResult(c, "key", "string", k, d) && HoneywellBarcodeReaderUtils.stdParamCheckResult(e, "option", "string", k, d) && HoneywellBarcodeReaderUtils.stdParamCheckResult(f,
                    "value", "string", k, d)) && (a = HoneywellBarcodeReaderUtils.getSettingDef(HowneywellBarcodeReaderSwiftSettings, a, c, e, f, !0), this.logVar("setBuffered settingDef", a, !1), this.batchSetBuffer.push(a), 0 === a.status ? HoneywellBarcodeReaderUtils.isFunction(d) && (k.status = 0, k.message = "Set request successfully buffered.", setTimeout(function () { d(k) }, 0)) : HoneywellBarcodeReaderUtils.isFunction(d) && (k.status = a.status, k.message = a.message, setTimeout(function () { d(k) }, 0)))
        }, verifyActiveConnection: function (a) {
            return null ===
                this.sessionId || null === this.filter ? (HoneywellBarcodeReaderUtils.isFunction(a) && setTimeout(function () { a({ status: HoneywellBarcodeReaderErrors.NO_CONNECTION, message: "No scanner connection" }) }, 0), !1) : !0
        }
    }; HoneywellBarcodeReadersAjax = function (a) { HoneywellBarcodeReaderUtils.isFunction(a) && setTimeout(function () { a({ status: 0, message: HoneywellBarcodeReaderUtils.MSG_OPERATION_COMPLETED }) }, 0) }; HoneywellBarcodeReadersAjax.prototype = {
        version: "1.20.00.0825", getAvailableBarcodeReaders: function (a) {
            function c(c) {
                if (HoneywellBarcodeReaderUtils.hasProperty(c,
                    "result", !0) && HoneywellBarcodeReaderUtils.hasProperty(c.result, "scanners", !0) && c.result.scanners instanceof Array) for (var b = 0, d = c.result.scanners.length; b < d; b++)HoneywellBarcodeReaderUtils.hasProperty(c.result.scanners[b], "scanner", !0) && e.push(c.result.scanners[b].scanner); 0 === e.length && e.push("dcs.scanner.imager"); HoneywellBarcodeReaderUtils.isFunction(a) && setTimeout(function () { a(e) }, 0)
            } var e = [], f = { method: "scanner.listConnectedScanners" }; if (HoneywellBarcodeReaderUtils.isFunction(a)) HoneywellBarcodeReaderUtils.sendJsonRpcRequestSubSys("datacollection",
                f, c); else { var d; f.id = HoneywellBarcodeReaderUtils.getRandomInt(1E4, 99999); f.jsonrpc = "2.0"; window.XMLHttpRequest && (d = new XMLHttpRequest, d.onreadystatechange = function () { if (4 == d.readyState && 200 == d.status) { var a; try { a = JSON.parse(d.responseText), c(a) } catch (b) { } } }, d.open("POST", "http://127.0.0.1:8080/jsonrpc/datacollection", !1), d.send(JSON.stringify(f))) } return e
        }
    }
})();