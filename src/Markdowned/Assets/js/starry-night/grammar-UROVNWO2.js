import"./grammar-MHCU5XT2.js";var e={extensions:[".rego"],names:["open-policy-agent"],patterns:[{include:"#comment"},{include:"#keyword"},{include:"#comparison-operators"},{include:"#assignment-operators"},{include:"#term"}],repository:{"assignment-operators":{match:":\\=|\\=",name:"keyword.operator.assignment.rego"},call:{captures:{1:{name:"support.function.any-method.rego"}},match:"([a-zA-Z_][a-zA-Z0-9_]*)\\(",name:"meta.function-call.rego"},comment:{patterns:[{captures:{1:{name:"punctuation.definition.comment.rego"},2:{name:"strong"}},match:"(#)\\s*(METADATA)\\s*$\\n?",name:"comment.line.number-sign.rego"},{captures:{1:{name:"punctuation.definition.comment.rego"},2:{name:"strong"}},match:"(#)\\s*(scope|title|description|related_resources|authors|organizations|schemas|entrypoint|custom):.*$\\n?",name:"comment.line.number-sign.rego"},{captures:{1:{name:"punctuation.definition.comment.rego"}},match:"(#).*$\\n?",name:"comment.line.number-sign.rego"}]},"comparison-operators":{match:"\\=\\=|\\!\\=|>|<|<\\=|>\\=|\\+|-|\\*|%|/|\\||&",name:"keyword.operator.comparison.rego"},constant:{match:"\\b(?:true|false|null)\\b",name:"constant.language.rego"},"interpolated-string-double":{begin:'(\\$)(")',beginCaptures:{1:{name:"punctuation.definition.template-expression.begin.rego"},2:{name:"punctuation.definition.string.begin.rego"}},end:'"',endCaptures:{0:{name:"punctuation.definition.string.end.rego"}},name:"string.template.rego",patterns:[{include:"#interpolation-expression"},{include:"#string-escape"},{include:"#interpolation-escape"},{include:"#string-escape-invalid"}]},"interpolated-string-raw":{begin:"(\\$)(`)",beginCaptures:{1:{name:"punctuation.definition.template-expression.begin.rego"},2:{name:"punctuation.definition.string.begin.rego"}},end:"`",endCaptures:{0:{name:"punctuation.definition.string.end.rego"}},name:"string.template.rego",patterns:[{include:"#interpolation-expression"}]},"interpolation-escape":{match:"\\\\[{}]",name:"constant.character.escape.rego"},"interpolation-expression":{begin:"(?<!\\\\)\\{",beginCaptures:{0:{name:"punctuation.section.embedded.rego"}},end:"\\}",endCaptures:{0:{name:"punctuation.section.embedded.rego"}},name:"meta.embedded.expression.rego",patterns:[{include:"#interpolation-expression-contents"}]},"interpolation-expression-contents":{patterns:[{include:"#comment"},{include:"#constant"},{include:"#string"},{include:"#number"},{include:"#call"},{include:"#root-document"},{include:"#variable"},{include:"#comparison-operators"}]},keyword:{match:"(^|\\s+)(?:(default|not|package|import|as|with|else|some|in|every|if|contains|and|or))(?=\\s|$)",name:"keyword.other.rego"},number:{match:`(?x:                # turn on extended mode
                             -?         # an optional minus
                             (?:
                               0        # a zero
                               |        # ...or...
                               [1-9]    # a 1-9 character
                               \\d*      # followed by zero or more digits
                             )
                             (?:
                               (?:
                                 \\.     # a period
                                 \\d+    # followed by one or more digits
                               )?
                               (?:
                                 [eE]   # an e character
                                 [+-]?  # followed by an option +/-
                                 \\d+    # followed by one or more digits
                               )?       # make exponent optional
                             )?         # make decimal portion optional
                           )`,name:"constant.numeric.rego"},"root-document":{match:"(?<!\\.)\\b(?:input|data)\\b",name:"variable.other.constant.rego support.constant.rego variable.language.rego"},string:{patterns:[{include:"#interpolated-string-double"},{include:"#interpolated-string-raw"},{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin.rego"}},end:'"',endCaptures:{0:{name:"punctuation.definition.string.end.rego"}},name:"string.quoted.double.rego",patterns:[{include:"#string-escape"},{include:"#string-escape-invalid"}]},{begin:"`",beginCaptures:{0:{name:"punctuation.definition.string.begin.rego"}},end:"`",endCaptures:{0:{name:"punctuation.definition.string.end.rego"}},name:"string.other.raw.rego"}]},"string-escape":{match:`(?x:       # turn on extended mode
			\\\\                 # a literal backslash
			(?:                # ...followed by...
				["\\\\/bfnrt]    # one of these characters
				|              # ...or...
				u              # a u
				[0-9a-fA-F]{4} # and four hex digits
			)
			)`,name:"constant.character.escape.rego"},"string-escape-invalid":{match:"\\\\.",name:"invalid.illegal.unrecognized-string-escape.rego"},term:{patterns:[{include:"#constant"},{include:"#string"},{include:"#number"},{include:"#call"},{include:"#root-document"},{include:"#variable"}]},variable:{match:"\\b[[:alpha:]_][[:alnum:]_]*\\b",name:"meta.identifier.rego"}},scopeName:"source.rego"},n=e;export{n as default};
