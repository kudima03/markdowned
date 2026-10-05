import"./grammar-MHCU5XT2.js";var t={extensions:[".pwn",".sma"],names:["pawn"],patterns:[{include:"#translation_unit"}],repository:{block:{begin:"(?=\\{)",end:"\\}",patterns:[{include:"#block-lookahead-end"}]},"block-lookahead-end":{begin:"\\{",end:"(?=\\})",name:"meta.block.c",patterns:[{include:"#lex"},{include:"#call"},{include:"#support"},{include:"#function"},{include:"$base"}]},call:{begin:`(?x)
				\\s*
				(?= # don't consume to recognize support functions
				    (?: [A-Za-z_@]\\w*+ | ::[^:] )++
				    (?:\\s|/\\*.*?\\*/)*+ \\( )
			`,end:"\\)",name:"meta.function-call.c",patterns:[{include:"#lex"},{match:"(?:(?<=\\.)|(?<=->))\\b([A-Za-z_@]\\w*+)\\b",name:"variable.other.dot-access.c support.function.any-method.c"},{match:"(?:[A-Za-z_@]\\w*+|::[^:])++",name:"support.function.any-method.c"},{include:"#parens-lookahead-end"}]},"comment-banner-line":{captures:{1:{name:"meta.toc-list.banner.c"},3:{name:"punctuation.whitespace.newline.c"}},match:"(?:(?<=//)|(?<=/\\*)|^)[\\s/*]*(=+\\s*(.*?)\\s*=+(?:(?=[\\s/*+\\-]*\\*/)|$(\\n?)))"},"comment-innards":{patterns:[{include:"#comment-banner-line"},{include:"#comment-task-tag-line"},{include:"#lex-continuation"},{include:"#lex-newline"}]},"comment-task-tag-line":{patterns:[{begin:`(?ix)
					    (?= (?-i: @[a-zA-Z_]++ | \\b [A-Z_]++) \\b) @? \\b (?:
					        (FIXME) | (XXX) | (WTF)
					    ) \\b
					`,beginCaptures:{0:{name:"keyword.other.task-tag.prio-high.c"},1:{name:"storage.type.class.fixme.c"},2:{name:"storage.type.class.xxx.c"},3:{name:"storage.type.class.wtf.c"}},end:"(?=[\\s/*]*\\*/)|(?<=$\\n)",name:"meta.toc-list.task-tag.prio-high.c",patterns:[{include:"#comment-task-tag-line-innards"}]},{begin:`(?ix)
					    (?= (?-i: @[a-zA-Z_]++ | \\b [A-Z_]++) \\b) @? \\b (?:
					        (TODO)
					    ) \\b
					`,beginCaptures:{0:{name:"keyword.other.task-tag.prio-normal.c"},1:{name:"storage.type.class.todo.c"}},end:"(?=[\\s/*]*\\*/)|(?<=$\\n)",name:"meta.toc-list.task-tag.prio-normal.c",patterns:[{include:"#comment-task-tag-line-innards"}]},{begin:`(?ix)
					    (?= (?-i: @[a-zA-Z_]++ | \\b [A-Z_]++) \\b) @? \\b (?:
					        (TBD) | (REVIEW)
					    ) \\b
					`,beginCaptures:{0:{name:"keyword.other.task-tag.prio-low.c"},1:{name:"storage.type.class.tbd.c"},2:{name:"storage.type.class.review.c"}},end:"(?=[\\s/*]*\\*/)|(?<=$\\n)",name:"meta.toc-list.task-tag.prio-low.c",patterns:[{include:"#comment-task-tag-line-innards"}]},{begin:`(?ix)
					    (?= (?-i: @[a-zA-Z_]++ | \\b [A-Z_]++) \\b) @? \\b (?:
					        (NOTE) | (NB) | (CHANGED) | (IDEA) | (IMPORTANT) | (HACK) | (BUG)
					    ) \\b
					`,beginCaptures:{0:{name:"keyword.other.task-tag.note.c"},1:{name:"storage.type.class.note.c"},2:{name:"storage.type.class.nb.c"},3:{name:"storage.type.class.changed.c"},4:{name:"storage.type.class.idea.c"},5:{name:"storage.type.class.important.c"},6:{name:"storage.type.class.hack.c"},7:{name:"storage.type.class.bug.c"}},end:"(?=[\\s/*]*\\*/)|(?<=$\\n)",name:"meta.toc-list.task-tag.note.c",patterns:[{include:"#comment-task-tag-line-innards"}]}]},"comment-task-tag-line-innards":{patterns:[{include:"#comment-task-tag-line"},{include:"#lex-continuation"},{include:"#lex-newline"}]},comments:{patterns:[{begin:"\\s*(/\\*)",captures:{1:{name:"punctuation.definition.comment.block.c"}},end:"(\\*/)(\\n?)",endCaptures:{2:{name:"punctuation.whitespace.newline.c"}},name:"comment.block.c",patterns:[{include:"#comment-innards"}]},{match:"\\*/(?![/*])",name:"invalid.illegal.stray-comment-end.c"},{begin:"\\s*(//)",beginCaptures:{1:{name:"punctuation.definition.comment.line.double-slash.c++"}},end:"(?<=$\\n)(?<!\\\\$\\n)",name:"comment.line.double-slash.c++",patterns:[{include:"#comment-innards"}]}]},function:{patterns:[{include:"#function-fixup-macro"},{include:"#function-declaration"},{include:"#function-definition"}]},"function-declaration":{begin:`(?x)
				(?: ^
				  | (?<! (?<!\\w) new
				        | (?<!\\w) (?:else|enum) | (?<!\\w) (?:class|union)
				        | (?<!\\w) (?:struct|return|sizeof|typeof)
				        | (?<!\\w) __typeof | (?<!\\w) __typeof__ )
				    (?<= \\w ) \\s

				  | #  or type modifier / closing bracket before name
				    (?<= [^&]& | [*>)}\\]] ) ) \\s*

				(   (?: [A-Za-z_@]\\w*+ | ::[^:] )++
				    (?: (?<= ^ operator | \\W operator )  # C++ operator?
				        (?: [-*&<>=+!]+ | \\(\\) | \\[\\] ) )? )

				(?= (?:\\s|/\\*.*?\\*/)*+ (?'parens' \\(
				            (?> \\g'parens' |
				            	"(\\\\.|[^"])*" | '(\\\\.|[^'])*' | /\\*.*?\\*/ |
				            	(?! /[/*] | [()] ) . )*
				        \\) ) \\s* ; )
			`,beginCaptures:{1:{name:"entity.name.function.declaration.c"}},end:";",name:"meta.function.c",patterns:[{include:"#lex"},{include:"#parens"}]},"function-definition":{begin:`(?x)
				(?: ^
				  | (?<! (?<!\\w) new
				        | (?<!\\w) (?:else|enum) | (?<!\\w) (?:class|union)
				        | (?<!\\w) (?:struct|return|sizeof|typeof)
				        | (?<!\\w) __typeof | (?<!\\w) __typeof__ )
				    (?<= \\w ) \\s

				  | #  or type modifier / closing bracket before name
				    (?<= [^&]& | [*>)}\\]\\:] ) ) \\s*

				(   (?: [A-Za-z_@]\\w*+ | ::[^:] )++
				    (?: (?<= ^ operator | \\W operator )  # C++ operator?
				        (?: [-*&<>=+!]+ | \\(\\) | \\[\\] ) )? )

				(?= (?:\\s|/\\*.*?\\*/)*+ \\( )
			`,beginCaptures:{1:{name:"entity.name.function.definition.c"}},end:"\\}|;",name:"meta.function.c",patterns:[{include:"#lex"},{include:"#parens"},{match:"\\s*\\b(const|override)\\b",name:"storage.modifier.c"},{include:"#block-lookahead-end"}]},"function-fixup-macro":{begin:`(?x)
				^ # Begin of line, capital letters: most probably it is a macro
				\\s*\\b
				([A-Z0-9_]++)
				\\b
				(?= (?:\\s|/\\*.*?\\*/)*+ \\( )
			`,end:"\\)",patterns:[{include:"#lex"},{include:"#parens-lookahead-end"}]},lex:{patterns:[{include:"#lex-in-preprocessor"},{include:"#preprocessor"}]},"lex-constant":{patterns:[{captures:{1:{name:"constant.language.c"}},match:"\\s*\\b(true|false|TRUE|FALSE)\\b"}]},"lex-continuation":{patterns:[{captures:{1:{name:"keyword.other.line-continuation.c"},2:{name:"punctuation.whitespace.newline.c"}},match:"(\\\\)$(\\n?)",name:"punctuation.separator.continuation.c"},{captures:{1:{name:"invalid.deprecated.space-after-continuation.c"}},match:"\\\\(\\s+?)(?=\\n)$"}]},"lex-core":{patterns:[{include:"#comments"},{include:"#lex-continuation"},{include:"#lex-newline"},{include:"#lex-number"},{include:"#lex-string"}]},"lex-in-preprocessor":{patterns:[{include:"#lex-core"},{include:"#lex-keyword"},{include:"#support-keyword"},{include:"#lex-constant"}]},"lex-keyword":{patterns:[{captures:{1:{name:"keyword.other.preprocessor.c"}},match:"\\s*\\b(defined)\\b"},{captures:{1:{name:"keyword.operator.c"}},match:"\\s*\\b(sizeof|tagof)\\b"},{captures:{2:{name:"invalid.illegal.invalid-indentation"}},match:"(Iterator:)(\\t)"},{begin:"^\\s*(case)\\s+",beginCaptures:{1:{name:"keyword.control.c"}},end:"(:)|(?<=^|[^\\\\])\\s*(\\n)",endCaptures:{1:{name:"keyword.operator.ternary.c"}},patterns:[{include:"#lex-core"}]},{captures:{1:{name:"keyword.control.c"}},match:"\\s*\\b(assert|break|case|continue|default|do|else|exit|for|goto|if|return|sleep|state|switch|while)\\b"},{captures:{1:{name:"storage.type.c"}},match:"\\s*\\b(new|enum)\\b"},{captures:{1:{name:"storage.modifier.c"}},match:"\\s*\\b(public|forward|native|char|const|static|stock|hook|task|ptask)\\b"},{match:"([A-Za-z_]\\w*)\\:",name:"storage.modifier.c"},{match:"(\\-|\\+|\\*|\\/|%|&|\\||\\^|<<|>>)?=",name:"keyword.operator.assignment.c"},{match:"(==|!=|<=|>=|<>|<|>)",name:"keyword.operator.comparison.c"},{match:"(\\-\\-|\\+\\+)",name:"keyword.operator.increment-decrement.c"},{match:"(\\?|:)",name:"keyword.operator.ternary.c"},{match:"(\\-|\\+|\\*|\\/|%)",name:"keyword.operator.arithmetic.c"},{match:"(!|&&|\\|\\|)",name:"keyword.operator.logical.c"},{match:"(~|&|\\||\\^|<<|>>)",name:"keyword.operator.bitwise.c"}]},"lex-newline":{match:"$\\n",name:"punctuation.whitespace.newline.c"},"lex-number":{patterns:[{captures:{1:{name:"constant.numeric.integer.decimal.c"},2:{name:"keyword.operator.switch-range.c"},3:{name:"constant.numeric.integer.decimal.c"}},match:"([0-9]+)(\\.{2})([0-9]+)"},{captures:{1:{name:"storage.type.number.prefix.hexadecimal.c"},2:{name:"invalid.illegal.number.missing-fragment.significand.c"},3:{name:"invalid.illegal.numeric-literal-character.float.whole-number.c"},4:{name:"invalid.illegal.numeric-literal-character.float.fraction.c"},5:{name:"keyword.other.exponent.hexadecimal.c"},6:{name:"invalid.illegal.numeric-literal-character.float.exponent.c"},7:{name:"invalid.illegal.numeric-literal-character.float.exponent.c"},8:{name:"invalid.illegal.number.missing-fragment.exponent.c"},9:{name:"storage.type.number.suffix.float.c"}},match:`(?ix)  # hexadecimal float
						(?<!\\.) \\b

						(0x)

						# significand
						(?: (\\.) (?=p)  # invalid
						  |        [0-9a-f]*+ ([0-9a-z]*?) [0-9a-f]*+
						    (?: \\. [0-9a-f]*+ ([0-9a-z.]*?) [0-9a-f]*+ )? )

						# exponent (required)
						(?: (p) (?:        [+\\-]  [0-9]++ ([0-9a-z]*?)
						          | (?=[0-9a-z.]) [0-9]*+ ([0-9a-z.]*?) )
						  | (p) )

						# remaining valid chars and type
						[0-9]*+ ([fl]?)

						\\b (?!\\.)
					`,name:"constant.numeric.float.hexadecimal.c"},{captures:{1:{name:"storage.type.number.prefix.hexadecimal.c"},2:{name:"invalid.illegal.numeric-literal-character.float.whole-number.c"},3:{name:"invalid.illegal.number.hexadecimal-float-requires-exponent.c"},4:{name:"invalid.illegal.numeric-literal-character.float.fraction.c"},5:{name:"storage.type.number.suffix.float.c"}},match:`(?ix)  # hexadecimal float without required exponent
						(?<!\\.) \\b

						(0x)

						# significand (at least a period)
						     [0-9a-f]*+ ([0-9a-z&&[^p]]*?) [0-9a-f]*+
						(\\.) [0-9a-f]*+ ([0-9a-z.&&[^p]]*?) [0-9a-f]*+

						# type
						(l?)

						(?:(?<=\\.)|\\b) (?!\\.)
					`,name:"constant.numeric.float.hexadecimal.c"},{captures:{1:{name:"invalid.illegal.numeric-literal-character.float.whole-number.c"},2:{name:"invalid.illegal.numeric-literal-character.float.fraction.c"},3:{name:"invalid.illegal.numeric-literal-character.float.whole-number.c"},4:{name:"keyword.other.exponent.decimal.c"},5:{name:"invalid.illegal.numeric-literal-character.float.exponent.c"},6:{name:"invalid.illegal.numeric-literal-character.float.exponent.c"},7:{name:"invalid.illegal.numeric-literal-character.float.exponent.c"},8:{name:"invalid.illegal.numeric-literal-character.float.exponent.c"},9:{name:"storage.type.number.suffix.float.c"}},match:`(?ix)  # decimal float literal
						(?<!\\.) (?:(?=\\.)|\\b)

						(?!0x)
						# significand
						(?: (?: [0-9]++ ([0-9a-z&&[^e]]*?) [0-9]*+ )?
						    \\.  [0-9]++ ([0-9a-z.&&[^e]]*?) [0-9]*+

						  |     [0-9]++ ([0-9a-z&&[^e]]*?) [0-9]*+ (?: \\. | (?=e)) )

						# exponent (optional)
						(?: (e) (?: [+\\-]  [0-9]++ ([0-9a-z]*?)
						          |        [0-9]++ ([0-9a-z.]*?) )
						  | ( p     [+\\-]? [0-9]++
						    | [ep]                  [0-9a-z.]*?) )?

						# any invalid chars and type
						([0-9a-z]*?) [0-9]*+ ([fl]?)

						(?:(?<=\\.)|\\b) (?!\\.)
					`,name:"constant.numeric.float.c"},{captures:{1:{name:"storage.type.number.prefix.hexadecimal.c"},2:{name:"storage.type.number.suffix.c"}},match:`(?ix)
						(?<!\\.) \\b
						(0x)? 0++
						(u?l{0,2}|lul?|llu)
						\\b (?!\\.)
					`,name:"constant.numeric.integer.zero.c"},{match:`(?ix)
						(?<!\\.) \\b

						(?: (0x) | (0b) )
						(u?l{0,2}|lul?|llu)

						\\b (?!\\.)
					`,name:"invalid.illegal.invalid-number-literal.c"},{captures:{1:{name:"storage.type.number.prefix.hexadecimal.c"},2:{name:"invalid.illegal.numeric-literal-character.integer.c"},3:{name:"storage.type.number.suffix.c"}},match:`(?ix)
						(?<!\\.) \\b

						(0x) [0-9a-f]++

						# any invalid chars
						([0-9a-z]*?)

						# the remainder (after invalid chars, if any) and a type
						[0-9a-f]* (u?l{0,2}|lul?|llu)

						\\b (?!\\.)
					`,name:"constant.numeric.integer.hexadecimal.c"},{captures:{1:{name:"storage.type.number.prefix.binary.c"},2:{name:"invalid.illegal.numeric-literal-character.integer.c"},3:{name:"storage.type.number.suffix.c"}},match:`(?ix)
						(?<!\\.) \\b

						(0b) [01]++

						# any invalid chars
						([0-9a-z]*?)

						# the remainder (after invalid chars, if any) and a type
						[01]* (u?l{0,2}|lul?|llu)

						\\b (?!\\.)
					`,name:"constant.numeric.integer.binary.c"},{captures:{1:{name:"storage.type.number.prefix.octal.c"},2:{name:"invalid.illegal.numeric-literal-character.integer.c"},3:{name:"storage.type.number.suffix.c"}},match:`(?ix)
						(?<!\\.) \\b

						(0) [0-7]++

						# any invalid chars
						([0-9a-z]*?)

						# the remainder (after invalid chars, if any) and a type
						[0-7]* (u?l{0,2}|lul?|llu)

						\\b (?!\\.)
					`,name:"constant.numeric.integer.octal.c"},{captures:{1:{name:"invalid.illegal.numeric-literal-character.integer.c"},2:{name:"storage.type.number.suffix.c"}},match:`(?ix)
						\\b

						[0-9][0-9_]*

						# any invalid chars
						([0-9a-z]*?)

						# the remainder (after invalid chars, if any) and a type
						[0-9]* (u?l{0,2}|lul?|llu)

						\\b
					`,name:"constant.numeric.integer.decimal.c"}]},"lex-string":{patterns:[{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin.c"}},end:'(")|(?<=^|[^\\\\])\\s*(\\n)',endCaptures:{1:{name:"punctuation.definition.string.end.c"},2:{name:"invalid.illegal.unexpected-end-of-line.c"}},name:"string.quoted.double.c",patterns:[{include:"#lex-continuation"},{include:"#string_escaped_char"},{include:"#string_placeholder"}]},{begin:"'",beginCaptures:{0:{name:"punctuation.definition.string.begin.c"}},end:"(')|(?<=^|[^\\\\])\\s*(\\n)",endCaptures:{1:{name:"punctuation.definition.string.end.c"},2:{name:"invalid.illegal.unexpected-end-of-line.c"}},name:"string.quoted.single.c",patterns:[{include:"#lex-continuation"},{include:"#string_escaped_char"}]}]},parens:{begin:"(?=\\()",end:"\\)",patterns:[{include:"#parens-lookahead-end"}]},"parens-lookahead-end":{begin:"\\(",end:"(?=\\))",name:"meta.parens.c",patterns:[{include:"#lex"},{include:"#call"},{include:"#support"},{include:"$base"}]},"ppline-any":{begin:"^\\s*(#)",beginCaptures:{0:{name:"keyword.other.preprocessor.c"}},end:"(?<=$\\n)(?<!\\\\$\\n)",name:"meta.preprocessor.directive.null-directive.c",patterns:[{include:"#lex-core"}]},"ppline-directive":{begin:"^\\s*(#)\\s*(if|elseif|else|endif|pragma|line|define|undef|section|assert|file|endinput|endscript)\\b",beginCaptures:{0:{name:"keyword.other.preprocessor.c"}},end:"(?<=$\\n)(?<!\\\\$\\n)",name:"meta.preprocessor.directive.c",patterns:[{include:"#lex-core"},{include:"#lex-in-preprocessor"}]},"ppline-directive-emit":{begin:`(?x)
				^\\s*(\\#|@)\\s*(emit) #pre-processor directive
				(\\s+
					([A-Z0-9a-z]+)
					(
						(\\.)([A-Za-z]+)
						((\\.)([A-Za-z]+))?
					)?
				|\\s*)
			`,beginCaptures:{1:{name:"keyword.other.preprocessor.c"},2:{name:"keyword.control.import.c"},4:{name:"entity.name.function.preprocessor.c"},6:{name:"punctuation.separator.parameters.c"},7:{name:"entity.name.function.preprocessor.c"}},end:"(?<=$\\n)(?<!\\\\$\\n)",name:"meta.preprocessor.directive.emit.c",patterns:[{include:"#lex-core"},{include:"#lex-in-preprocessor"}]},"ppline-directive-invalid-usage":{captures:{1:{name:"keyword.other.preprocessor.c"},4:{name:"invalid.illegal.invalid-usage-of-preprocessor-directive.c"}},match:"(^\\s*(#)\\s*(if|elseif|pragma|define|undef|include|tryinclude)\\b)\\s*?(\\n|$)",name:"meta.preprocessor.directive.c"},"ppline-error":{begin:"^\\s*(#)\\s*(error|warning)\\b",beginCaptures:{0:{name:"keyword.other.preprocessor.include.c"}},end:"(.*)|(?<=$\\n)(?<!\\\\$\\n)",endCaptures:{1:{name:"string.quoted.double.c"}},name:"meta.preprocessor.include.c meta.preprocessor.c.include"},"ppline-include":{begin:"^\\s*(#)\\s*(include|tryinclude)\\b",beginCaptures:{0:{name:"keyword.other.preprocessor.include.c"}},end:'(?:("[^"]*?)|(<[^>]*?))(\\n)|(?<=$\\n)(?<!\\\\$\\n)',endCaptures:{1:{name:"string.quoted.double.include.c"},2:{name:"string.quoted.other.lt-gt.include.c"},3:{name:"invalid.illegal.unexpected-end-of-line.c"}},name:"meta.preprocessor.include.c meta.preprocessor.c.include",patterns:[{include:"#ppline-include-innards"}]},"ppline-include-innards":{patterns:[{include:"#preprocessor-lex"},{begin:'"|(?=.*?")',beginCaptures:{0:{name:"punctuation.definition.string.begin.c"}},end:'"|(?<=^|[^\\\\])(?=\\s*\\n)',endCaptures:{0:{name:"punctuation.definition.string.end.c"}},name:"string.quoted.double.include.c"},{begin:"<(?=.*?>)",beginCaptures:{0:{name:"punctuation.definition.string.begin.c"}},end:">|(?<=^|[^\\\\])(?=\\s*\\n)",endCaptures:{0:{name:"punctuation.definition.string.end.c"}},name:"string.quoted.other.lt-gt.include.c"},{begin:"\\(",end:"\\)|(?<=^|[^\\\\])(?=\\s*\\n)",name:"meta.parens.c",patterns:[{include:"#ppline-include-innards"}]}]},"ppline-invalid":{begin:"^\\s*(#)(?!\\s*(?=/[/*]|(?>\\\\\\s*\\n)|\\n|$))\\s*(\\w*)",beginCaptures:{1:{name:"keyword.other.preprocessor.c"},2:{name:"invalid.illegal.preprocessor.c"}},end:"(?<=$\\n)(?<!\\\\$\\n)",name:"meta.preprocessor.directive.illegal.c"},"ppline-macro":{begin:"^\\s*(#)(?=\\s*(define)\\s+[a-zA-Z_]\\w*+)",beginCaptures:{0:{name:"keyword.other.preprocessor.c"}},end:"(?<=$\\n)(?<!\\\\$\\n)",name:"meta.preprocessor.macro.c",patterns:[{captures:{1:{name:"keyword.other.preprocessor.c"}},match:"\\s*(##)"},{captures:{1:{name:"keyword.other.preprocessor.c"},2:{name:"string.macro.stringify.c"}},match:"\\s*(#)\\s*([a-zA-Z_]\\w*+)"},{include:"#ppline-macro-head-function"},{include:"#ppline-macro-head-object"},{include:"#ppline-macro-param"},{include:"#lex-in-preprocessor"},{include:"#support"}]},"ppline-macro-head-function":{begin:"(?<!##)(?<=#)(\\s*define)\\s+([a-zA-Z_]\\w*+)(\\()",beginCaptures:{1:{name:"keyword.other.preprocessor.define.c"},2:{name:"entity.name.function.preprocessor.c"},3:{name:"meta.preprocessor.macro.parameters.c"}},contentName:"meta.preprocessor.macro.parameters.c",end:"(?<=\\))|(?<=^|[^\\\\])\\s*(\\n)?",endCaptures:{1:{name:"invalid.illegal.unexpected-end-of-line.c"}},patterns:[{include:"#ppline-macro-param"}]},"ppline-macro-head-object":{captures:{1:{name:"keyword.other.preprocessor.define.c"},2:{name:"entity.name.constant.preprocessor.c"}},match:"(?<!##)(?<=#)(\\s*define)\\s+([a-zA-Z_]\\w*+)(?!\\()[\\s&&[^\\n]]*"},"ppline-macro-param":{captures:{1:{name:"variable.parameter.c"}},match:"(%[0-9]+)"},"ppline-pragma-mark":{begin:"(^\\s*(#)\\s*(pragma\\s+(align|amxlimit|amxram|codepage|compress|ctrlchar|deprecated|dynamic|library|overlay|pack|rational|semicolon|tabsize|unused))\\b)[\\s&&[^\\n]]*",beginCaptures:{1:{name:"keyword.other.preprocessor.pragma.c"}},end:"(?<=$\\n)(?<!\\\\$\\n)",name:"meta.preprocessor.directive.c",patterns:[{include:"#lex-core"}]},"ppline-undef":{begin:"(^\\s*(#)\\s*(undef))\\s+([a-zA-Z_]\\w*+)",beginCaptures:{1:{name:"keyword.other.preprocessor.c"},4:{name:"variable.macro.undef.c"}},end:"(?<=$\\n)(?<!\\\\$\\n)",name:"meta.preprocessor.undef.c"},preprocessor:{begin:"(?=^\\s*(#))",end:"(?!^\\s*(#))",patterns:[{include:"#ppline-directive-invalid-usage"},{include:"#ppline-macro"},{include:"#ppline-undef"},{include:"#ppline-pragma-mark"},{include:"#ppline-include"},{include:"#ppline-error"},{include:"#ppline-directive"},{include:"#ppline-directive-obsolete"},{include:"#ppline-directive-emit"},{include:"#ppline-invalid"},{include:"#ppline-any"}]},"preprocessor-lex":{patterns:[{include:"#comments"},{include:"#lex-continuation"},{include:"#lex-newline"}]},string_escaped_char:{patterns:[{match:`\\\\(\\\\|[abefnprtv'"?]|[0-3]\\d{0,2}|[4-7]\\d?|x[a-fA-F0-9]{0,2}|u[a-fA-F0-9]{0,4}|U[a-fA-F0-9]{0,8})`,name:"constant.character.escape.c"},{match:"\\\\.",name:"invalid.illegal.unknown-escape.c"}]},string_placeholder:{patterns:[{match:`(?x)%
						(\\d+\\$)?                             # field (argument #)
						[#0\\- +']*                           # flags
						[,;:_]?                              # separator character (AltiVec)
						((-?\\d+)|\\*(-?\\d+\\$)?)?              # minimum field width
						(\\.((-?\\d+)|\\*(-?\\d+\\$)?)?)?         # precision
						[diouxXDOUeEfFgGaACcSspnq%]          # conversion type
					`,name:"constant.other.placeholder.c"},{match:"%",name:"invalid.illegal.placeholder.c"}]},support:{patterns:[{include:"#support-modifier"},{include:"#support-keyword"}]},"support-keyword":{patterns:[{captures:{1:{name:"keyword.control.c"}},match:"\\s*\\b(foreach)\\b"}]},"support-modifier":{patterns:[{captures:{1:{name:"storage.modifier.c"}},match:"\\s*\\b(inline|using)\\b"}]},translation_unit:{patterns:[{include:"#lex"},{include:"#function"},{include:"#support"},{include:"#block"},{include:"#parens"}]}},scopeName:"source.pawn"},e=t;export{e as default};
