import"./grammar-MHCU5XT2.js";var n={extensions:[".wast",".wat"],names:["webassembly","wast","wasm"],patterns:[{begin:"\\A(#!)",beginCaptures:{1:{name:"punctuation.definition.comment.hashbang.webassembly"}},end:"$",name:"comment.line.hashbang.webassembly"},{include:"#main"}],repository:{"comment-block":{begin:"\\(;",beginCaptures:{0:{name:"punctuation.section.comment.begin.webassembly"}},end:";\\)",endCaptures:{0:{name:"punctuation.section.comment.end.webassembly"}},name:"comment.block.semicolon.webassembly",patterns:[{include:"#comment-block"}]},"comment-line":{begin:";;",beginCaptures:{0:{name:"punctuation.definition.comment.webassembly"}},end:"$",name:"comment.line.semicolon.webassembly"},expression:{patterns:[{begin:"(\\()(\\w+)(?=[\\s()]|$|;;)",beginCaptures:{1:{name:"punctuation.section.expression.begin.webassembly"},2:{patterns:[{include:"#main"}]}},end:"\\)",endCaptures:{0:{name:"punctuation.section.expression.end.webassembly"}},name:"meta.expression.$2.webassembly",patterns:[{include:"#main"}]},{begin:"\\(",beginCaptures:{0:{name:"punctuation.section.expression.begin.webassembly"}},end:"\\)",endCaptures:{0:{name:"punctuation.section.expression.end.webassembly"}},name:"meta.expression.webassembly",patterns:[{include:"#main"}]}]},instructions:{patterns:[{match:`(?x) \\b
( block
| br(?:_if|_table|_on_(?:cast|data|func|i31|null))?
| call(?:_indirect|_ref)?
| catch_all
| catch
| delegate
| else
| end
| if
| loop
| nop
| rethrow
| return(?:_call(?:_indirect|_ref)?)?
| then
| throw
| try
| unreachable
| unwind
) (?=[\\s()]|$|;;)`,name:"keyword.control.instruction.$1.webassembly"},{captures:{1:{patterns:[{match:"\\.",name:"punctuation.separator.method.period.webassembly"}]}},match:`(?x) \\b
(
	(f32|f64) \\.
	( (?<=f32.) (demote_f64|reinterpret_i32)
	| (?<=f64.) (promote_f32|reinterpret_i64)
	| abs
	| add
	| atomic (?:\\.\\w+)++
	| ceil
	| const
	| convert_i(32|64)_[su]
	| copysign
	| div
	| eq
	| floor
	| [gl][et]
	| load
	| max
	| min
	| mul
	| nearest
	| neg?
	| ne
	| sqrt
	| store
	| sub
	| trunc
	)
	|
	
	(i32|i64) \\.
	( (?<=i32.) (reinterpret_f32|wrap_i64)
	| (?<=i64.) (extend_i32_[su]|extend32_s|load32_[su]|reinterpret_f64|store32)
	| add
	| and
	| atomic (?:\\.\\w+)++
	| clz
	| const
	| ctz
	| div_[su]
	| eqz?
	| eq
	| extend(8|16)_s
	| [gl][et]_[su]
	| load(8|16)_[su]
	| load
	| mul
	| ne
	| x?or
	| popcnt
	| rem_[su]
	| rot[lr]
	| shl
	| shr_[su]
	| store(8|16)?
	| sub
	| trunc_(sat_)?f(32|64)_[su]
	)
	|
	
	v128 \\.
	( andnot
	| and
	| any_true
	| atomic (?:\\.\\w+)++
	| bitselect
	| const
	| load((8|16)_lane|(32|64)_(lane|zero)|(8|16|32|64)_splat|(8x8|16x4|32x2)_[su])?
	| not
	| x?or
	| store((8|16|32|64)_lane)?
	)
	|

	(f32x4|f64x2) \\.
	( (?<=f32x4.) (convert_i32x4_[su]|demote_f64x2_zero)
	| (?<=f64x2.) (convert_low_i32x4_[su]|promote_low_f32x4)
	| abs
	| add
	| ceil
	| div
	| eq
	| (extract|replace)_lane
	| floor
	| [gl][et]
	| p?max
	| p?min
	| mul
	| nearest
	| neg?
	| splat
	| sqrt
	| sub
	| trunc
	)
	|
	
	i8x16 \\.
	( abs
	| (?:add|sub)_sat_[su]
	| add
	| all_true
	| avgr_u
	| bitmask
	| eq
	| extract_lane_[su]
	| [gl][et]_[su]
	| load_splat
	| max_[su]
	| min_[su]
	| narrow_i16x8_[su]
	| neg?
	| ne
	| popcnt
	| replace_lane
	| shl
	| shr_[su]
	| shuffle
	| splat
	| sub
	| swizzle
	)
	|
	
	i16x8 \\.
	( abs
	| (?:add|sub)_sat_[su]
	| add
	| all_true
	| avgr_u
	| bitmask
	| eq
	| ext(add_pairwise|(end|mul)_(high|low))_i8x16_[su]
	| extract_lane_[su]
	| [gl][et]_[su]
	| load8x8_[su]
	| load_splat
	| max_[su]
	| min_[su]
	| mul
	| narrow_i32x4_[su]
	| neg?
	| ne
	| q15mulr_sat_s
	| replace_lane
	| shl
	| shr_[su]
	| splat
	| sub
	)
	|

	i32x4 \\.
	( abs
	| add
	| all_true
	| bitmask
	| dot_i16x8_s
	| eq
	| ext(add_pairwise|(end|mul)_(high|low))_i16x8_[su]
	| extract_lane
	| [gl][et]_[su]
	| load16x4_[su]
	| load_splat
	| max_[su]
	| min_[su]
	| mul
	| neg?
	| ne
	| replace_lane
	| shl
	| shr_[su]
	| splat
	| sub
	| trunc_sat_f32x4_[su]
	| trunc_sat_f64x2_[su]_zero
	)
	|

	i64x2 \\.
	( abs
	| add
	| all_true
	| bitmask
	| eq
	| ext(end|mul)_(high|low)_i32x4_[su]
	| extract_lane
	| [gl][et]_s
	| load32x2_[su]
	| load_splat
	| mul
	| neg?
	| ne
	| replace_lane
	| shl
	| shr_[su]
	| splat
	| sub
	)
) (?=[\\s()]|$|;;)`,name:"entity.name.function.instruction.$1.webassembly"},{captures:{1:{name:"entity.name.function.instruction.$1.webassembly"},2:{name:"punctuation.separator.method.period.webassembly"},3:{name:"punctuation.separator.method.slash.webassembly"},4:{patterns:[{include:"#type"}]}},match:"\\b([if](?:32|64)(\\.)\\w+)(/)([if](?:32|64))"},{captures:{1:{patterns:[{match:"\\.",name:"punctuation.separator.method.period.webassembly"}]}},match:`(?x) \\b
( (array|struct) (\\. (len|new(_default)?_with_rtt|get_[su]|[gs]et))?
| (current|grow)_memory
| data\\.drop
| drop
| elem\\.drop
| field
| i31 \\. (new|get_[su])
| ([gs]et|tee)_(local|global)
| global\\.[gs]et
| local\\.([gs]et|tee)
| memory\\.(atomic\\.(notify|wait32|wait64)|copy|fill|grow|init|size)
| ref(\\.([ai]s_(data|func|i31|non_null)|cast|eq|func|is_null|null|test))?
| rtt(\\.(canon|sub))?
| select
| table\\.(copy|fill|get|grow|init|set|size)
) (?=[\\s()]|$|;;)`,name:"entity.name.function.instruction.$1.webassembly"},{match:`(?x) \\b
( anyfunc
| data
| declare
| element
| elem
| export
| extern
| func
| global
| import
| item
| local
| memory
| module
| mut
| offset
| param
| result
| start
| table
| type
) (?=[\\s()]|$|;;)`,name:"storage.type.$1.webassembly"},{captures:{1:{patterns:[{match:"\\.",name:"punctuation.separator.method.period.webassembly"}]}},match:`(?x) \\b
( assert_exhaustion
| assert_invalid
| assert_malformed
| assert_return (?:_(?:arithmetic|canonical)_nan)?
| assert_trap
| assert_unlinkable
| binary
| input
| invoke
| get
| output
| quote
| ref\\.extern
| ref\\.host
| register
| script
) (?=[\\s()]|$|;;)`,name:"support.function.scripting.webassembly"}]},main:{patterns:[{include:"#comment-line"},{include:"#comment-block"},{include:"#expression"},{include:"#instructions"},{include:"#type"},{include:"#number"},{include:"#name"},{include:"#string"},{include:"#optional-immediate"}]},name:{captures:{1:{name:"punctuation.definition.variable.webassembly"}},match:"(\\$)[-A-Za-z0-9_.+*/\\\\^~=<>!?@#$%&|:'`]+",name:"variable.other.name.webassembly"},number:{patterns:[{captures:{1:{name:"punctuation.separator.payload.colon.webassembly"},2:{name:"constant.numeric.int.hex.payload.webassembly"}},match:"(?<!\\w)[-+]?nan(:)(0x(?!_)(?:_?[A-Fa-f0-9])+)",name:"constant.language.nan.with-payload.webassembly"},{match:"(?<!\\w)[-+]?(inf|nan)(?:(?<=nan):(arithmetic|canonical))?(?!\\w)",name:"constant.language.$1.$2.webassembly"},{match:`(?x) (?<!\\w)
[-+]? 0x      (?!_)(?:_? [A-Fa-f0-9])++
(?: \\.       (?!_)(?:_? [A-Fa-f0-9])*+ )?
(?: [pP][-+]? (?!_)(?:_? [0-9])++ )?`,name:"constant.numeric.float.hex.webassembly"},{match:`(?x) (?<!\\w)
[-+]?         (?!_)(?:_? \\d)++
(?: \\.       (?!_)(?:_? \\d)*+ )?
(?: [eE][-+]? (?!_)(?:_? \\d)++ )?`,name:"constant.numeric.float.decimal.webassembly"},{match:"(?<!\\w)[-+]?0x(?!_)(?:_?[A-Fa-f0-9])++",name:"constant.numeric.int.hex.webassembly"},{match:"(?<!\\w)[-+]?(?!_)(?:_?\\d)++",name:"constant.numeric.int.decimal.webassembly"}]},"optional-immediate":{captures:{1:{name:"variable.parameter.$1.webassembly"},2:{name:"keyword.operator.assignment.webassembly"}},match:"(?<!\\w)(align|offset)(=)(?=[-+]?(?:\\d|0x[\\dA-Fa-f]))"},string:{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin.webassembly"}},end:'"',endCaptures:{0:{name:"punctuation.definition.string.end.webassembly"}},name:"string.quoted.double.webassembly",patterns:[{match:"\\\\n",name:"constant.character.escape.newline.webassembly"},{match:"\\\\t",name:"constant.character.escape.tab.webassembly"},{match:"\\\\{2}",name:"constant.character.escape.backslash.webassembly"},{match:"\\\\'",name:"constant.character.escape.quote.single.webassembly"},{match:'\\\\"',name:"constant.character.escape.quote.double.webassembly"},{captures:{1:{name:"punctuation.definition.unicode-escape.begin.bracket.curly.webassembly"},2:{name:"punctuation.definition.unicode-escape.end.bracket.curly.webassembly"}},match:"\\\\[0-9A-Fa-f]{2}|\\\\u({)[A-Fa-f0-9]+(})",name:"constant.character.escape.hex.unicode.webassembly"}]},type:{patterns:[{match:"\\b([if](?:32|64)|v128|i8|i16)(?=[\\s()]|$|;;)",name:"storage.type.number.$1.webassembly"},{match:"\\b(f32x4|f64x2|i8x16|i16x8|i32x4|i64x2)(?=[\\s()]|$|;;)",name:"storage.type.simd.$1.webassembly"},{match:"\\b((?:any|data|eq|extern|func|i31)ref)(?=[\\s()]|$|;;)",name:"storage.type.reference.$1.webassembly"}]}},scopeName:"source.webassembly"},t=n;export{t as default};
