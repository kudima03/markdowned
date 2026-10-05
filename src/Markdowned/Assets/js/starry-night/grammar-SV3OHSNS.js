import"./grammar-MHCU5XT2.js";var e={dependencies:["source.regexp.posix"],extensions:[],names:[],patterns:[{include:"#comma"},{include:"#comment"},{include:"#esc"},{include:"#float"},{include:"#int"},{include:"#str"},{include:"#colon"},{include:"#eql"},{include:"#dot"}],repository:{bareword:{match:'[^"\\s][\\S]*',name:"string.unquoted.bareword"},base64:{match:"[A-Za-z0-9+/=]{4,}",name:"constant.numeric.base64"},bool:{match:"\\b(true|false|TRUE|FALSE)\\b",name:"constant.logical.bool.boolean.${1:/downcase}"},boolish:{match:"(?i)\\b(true|false|yes|no|on|off)\\b",name:"constant.logical.bool.boolean.${1:/downcase}"},bracket:{patterns:[{match:"\\{",name:"punctuation.definition.bracket.curly.brace.begin"},{match:"\\}",name:"punctuation.definition.bracket.curly.brace.end"},{match:"\\[",name:"punctuation.definition.bracket.square.begin"},{match:"\\]",name:"punctuation.definition.bracket.square.end"},{match:"\\(",name:"punctuation.definition.bracket.round.parenthesis.begin"},{match:"\\)",name:"punctuation.definition.bracket.round.parenthesis.end"},{match:"<",name:"punctuation.definition.bracket.angle.ascii.begin"},{match:">",name:"punctuation.definition.bracket.angle.ascii.end"},{match:"\u27E8",name:"punctuation.definition.bracket.angle.unicode.begin"},{match:"\u27E9",name:"punctuation.definition.bracket.angle.unicode.end"}]},colon:{match:":",name:"punctuation.delimiter.separator.colon"},comma:{match:",",name:"punctuation.separator.delimiter.comma"},comment:{patterns:[{include:"#commentHash"}]},commentHash:{begin:"#",beginCaptures:{0:{name:"punctuation.definition.comment"}},end:"$",name:"comment.line.number-sign"},commentSemi:{begin:";+",beginCaptures:{0:{name:"punctuation.definition.comment"}},end:"$",name:"comment.line.semicolon"},commentSlash:{begin:"//",beginCaptures:{0:{name:"punctuation.definition.comment"}},end:"$",name:"comment.line.double-slash"},commentStart:{patterns:[{include:"#commentStartHash"}]},commentStartHash:{begin:"^(?=#)",end:"(?!\\G)",patterns:[{include:"#commentHash"}]},commentStartSemi:{begin:"^(?=;)",end:"(?!\\G)",patterns:[{include:"#commentSemi"}]},commentStartSlash:{begin:"^(?=//)",end:"(?!\\G)",patterns:[{include:"#commentSlash"}]},dash:{match:"-",name:"punctuation.delimiter.separator.dash.hyphen"},dot:{match:"\\.",name:"punctuation.delimiter.separator.property.period.dot"},dotDec:{match:"(?:\\G|(?<!\\.)\\b)\\d+(?:\\.\\d+)+\\b(?!\\.)",name:"constant.numeric.other.dot-decimal"},dotPair:{match:"\\.\\.|\u2025",name:"keyword.operator.punctuation.dots.splat.range.spread.rest"},dotTrail:{match:"\\.{4,}",name:"punctuation.delimiter.separator.dotted.border.leader.dots"},dots:{patterns:[{include:"#ellipsis"},{include:"#dotPair"},{include:"#dot"}]},ellipsis:{match:"\\.{3}|\u2026",name:"keyword.operator.punctuation.ellipsis.splat.range.spread.rest"},email:{patterns:[{include:"#emailBracketed"},{include:"#emailQuoted"},{include:"#emailUnquoted"}]},emailBracketed:{patterns:[{captures:{1:{patterns:[{include:"#bracket"}]},2:{patterns:[{include:"#emailInnards"}]},3:{patterns:[{include:"#bracket"}]}},match:"(<)\\s*([^>@\\s]+@[^>@\\s]+)\\s*(>)",name:"meta.email-address.bracketed.ascii.angle-brackets"},{captures:{1:{patterns:[{include:"#bracket"}]},2:{patterns:[{include:"#emailInnards"}]},3:{patterns:[{include:"#bracket"}]}},match:"(\u27E8)\\s*([^\u27E9@\\s]+@[^\u27E9@\\s]+)\\s*(\u27E9)",name:"meta.email-address.bracketed.unicode.angle-brackets"},{captures:{1:{patterns:[{include:"#bracket"}]},2:{patterns:[{include:"#emailInnards"}]},3:{patterns:[{include:"#bracket"}]}},match:"(\xAB)\\s*([^\xBB@\\s]+@[^\xBB@\\s]+)\\s*(\xBB)",name:"meta.email-address.bracketed.guillemots"},{captures:{1:{patterns:[{include:"#bracket"}]},2:{patterns:[{include:"#emailInnards"}]},3:{patterns:[{include:"#bracket"}]}},match:"(\\()\\s*([^\\)@\\s]+@[^\\)@\\s]+)\\s*(\\))",name:"meta.email-address.bracketed.round-brackets"},{captures:{1:{patterns:[{include:"#bracket"}]},2:{patterns:[{include:"#emailInnards"}]},3:{patterns:[{include:"#bracket"}]}},match:"({)\\s*([^}@\\s]+@[^}@\\s]+)\\s*(})",name:"meta.email-address.bracketed.curly-brackets"},{captures:{1:{patterns:[{include:"#bracket"}]},2:{patterns:[{include:"#emailInnards"}]},3:{patterns:[{include:"#bracket"}]}},match:"(\\[)\\s*([^\\]@\\s]+@[^\\]@\\s]+)\\s*(\\])",name:"meta.email-address.bracketed.square-brackets"}]},emailInnards:{captures:{0:{patterns:[{captures:{1:{name:"meta.local-part"},2:{name:"punctuation.separator.at-sign.email"},3:{name:"meta.domain"}},match:"\\G([^@]*)(@)(.*)"}]}},match:"(?:\\G|^|(?<=\\n)).+",name:"constant.other.reference.link.underline.email"},emailQuoted:{patterns:[{captures:{0:{name:"string.quoted.double"},1:{name:"punctuation.definition.string.begin.email-address"},2:{patterns:[{include:"#emailInnards"}]},3:{name:"punctuation.definition.string.end.email-address"}},match:'(")\\s*([^"@\\s]+@[^"@\\s]+)\\s*(")',name:"meta.email-address.quoted.ascii.double-quotes"},{captures:{0:{name:"string.quoted.double"},1:{name:"punctuation.definition.string.begin.email-address"},2:{patterns:[{include:"#emailInnards"}]},3:{name:"punctuation.definition.string.end.email-address"}},match:"(\u201C)\\s*([^\u201D@\\s]+@[^\u201D@\\s]+)\\s*(\u201D)",name:"meta.email-address.quoted.unicode.double-quotes"},{captures:{0:{name:"string.quoted.single"},1:{name:"punctuation.definition.string.begin.email-address"},2:{patterns:[{include:"#emailInnards"}]},3:{name:"punctuation.definition.string.end.email-address"}},match:"(\u2018)\\s*([^\u2019@\\s]+@[^\u2019@\\s]+)\\s*(\u2019)",name:"meta.email-address.quoted.unicode.single-quotes"},{captures:{0:{name:"string.quoted.template.backticks"},1:{name:"punctuation.definition.string.begin.email-address"},2:{patterns:[{include:"#emailInnards"}]},3:{name:"punctuation.definition.string.end.email-address"}},match:"(`)\\s*([^`@\\s]+@[^`@\\s]+)\\s*(`)",name:"meta.email-address.quoted.backticks"},{captures:{0:{name:"string.quoted.single"},1:{name:"punctuation.definition.string.begin.email-address"},2:{patterns:[{include:"#emailInnards"}]},3:{name:"punctuation.definition.string.end.email-address"}},match:"(`|')\\s*([^'@\\s]+@[^'@\\s]+)\\s*(')",name:"meta.email-address.quoted.single-quotes"}]},emailUnquoted:{captures:{1:{name:"string.unquoted.email-address",patterns:[{include:"#emailInnards"}]}},match:`(?x)
((?!\\.) (?:[^\\[\\(<\u27E8\xAB"'\\s@.]|\\.(?!\\.))++ @
([^\\[\\(<\u27E8\xAB"'\\s@.]+?\\.(?=[^\\.\\s])(?:[^\\[\\(<\u27E8\xAB"'\\s@.]|\\.(?!\\.))++))`,name:"meta.email-address.unquoted"},eql:{match:"=",name:"keyword.operator.assignment.key-value.equals-sign"},esc:{captures:{1:{name:"punctuation.definition.escape.backslash"}},match:"(\\\\).",name:"constant.character.escape.backslash"},float:{patterns:[{include:"#floatExp"},{include:"#floatNoExp"}]},floatExp:{match:"[-+]?(?:[0-9]*\\.[0-9]+|[0-9]+\\.)(?:[eE][-+]?[0-9]+)++",name:"constant.numeric.float.real.decimal.dec.exponential.scientific"},floatNoExp:{match:"[-+]?(?:[0-9]*\\.[0-9]+|[0-9]+\\.)++",name:"constant.numeric.float.real.decimal.dec"},glob:{patterns:[{include:"#globSimple"},{include:"#globSet"},{include:"#globBraces"}]},globBraces:{patterns:[{include:"#globBracesSeq"},{include:"#globBracesAlt"}]},globBracesAlt:{captures:{0:{patterns:[{include:"#globBracesSeq"},{captures:{0:{patterns:[{include:"#esc"}]}},match:"{(?:\\.?+(?:[^{},.\\\\]|\\\\.))*+}"},{include:"#esc"},{include:"#globSet"},{include:"#globSimple"},{captures:{1:{patterns:[{include:"#bracket"}]},2:{patterns:[{include:"#comma"}]}},match:"({|})|(,)"}]}},match:`(?x)
(?<char>[^\\s{,}\\\\]|\\\\[{,}\\\\]|\\g<braced>){0}
(?<braced>{(?:[^{},\\\\]|\\\\.)*+}){0}
(?<alt>\\g<char>*+,\\g<char>*+|\\g<char>++){0}
(?<seq>{(?:-?\\d+\\.\\.-?\\d+|[a-zA-Z]\\.\\.[a-zA-Z])(?:\\.\\.-?\\d+)?}){0}
(?<entry> \\g<char>*+ (?:
	\\g<seq>+
	|
	(?!\\g<braced>)
	{
		(?<braces>
			\\g<alt>*+
			(?:(?!\\g<braced>) { \\g<braces>*+ } | \\g<seq>++)
			\\g<alt>*+
			|
			\\g<alt>++
		)
	}
) \\g<char>*+)`,name:"meta.brace-expansion.alternation"},globBracesSeq:{captures:{1:{patterns:[{include:"etc#bracket"}]},2:{name:"meta.range.numeric",patterns:[{include:"#dots"},{include:"#intNoExp"}]},3:{name:"meta.range.alphabetic",patterns:[{include:"#dots"},{match:"\\w",name:"constant.character.letter"}]},4:{name:"meta.increment",patterns:[{include:"#dots"},{include:"#intNoExp"}]},5:{patterns:[{include:"etc#bracket"}]}},match:"({)(?:(-?\\d+\\.\\.-?\\d+)|([a-zA-Z]\\.\\.[a-zA-Z]))(\\.\\.-?\\d+)?(})",name:"meta.brace-expansion.sequence"},globSet:{captures:{1:{name:"brackethighlighter.square.punctuation.definition.character-class.set.begin"},2:{name:"keyword.operator.logical.not"},3:{patterns:[{include:"#esc"},{captures:{1:{patterns:[{include:"#dash"}]},2:{name:"constant.character.single"}},match:"(?!^|\\G)(-)(?!\\])(-)?"},{include:"source.regexp.posix#charClass"},{include:"source.regexp.posix#localeClasses"},{match:".",name:"constant.character.single"}]},4:{name:"brackethighlighter.square.punctuation.definition.character-class.set.end"}},match:`(?x)
(\\[) (!|\\^)?
(
	(?: [^\\\\\\[\\]]
	|   \\\\.
	|   \\[ (?::[!^]?\\w+:|\\..+?\\.|=.+?=) \\]
	)*+
)(\\])`,name:"meta.character-class.set"},globSimple:{patterns:[{match:"\\*{2}",name:"keyword.operator.glob.wildcard.globstar"},{match:"[*?]",name:"keyword.operator.glob.wildcard"}]},hex:{match:"[-+]?[A-Fa-f0-9]+",name:"constant.numeric.integer.int.hexadecimal.hex"},hexNoSign:{match:"[A-Fa-f0-9]+",name:"constant.numeric.integer.int.hexadecimal.hex"},int:{patterns:[{include:"#intExp"},{include:"#intNoExp"}]},intExp:{match:"[-+]?[0-9]+[eE][-+]?[0-9]+",name:"constant.numeric.integer.int.decimal.dec.exponential.scientific"},intNoExp:{match:"[-+]?[0-9]+",name:"constant.numeric.integer.int.decimal.dec"},ip:{patterns:[{include:"#ipv6"},{include:"#ipv4"}]},ipv4:{captures:{1:{patterns:[{include:"#dot"}]},2:{name:"meta.cidr-notation"},3:{name:"keyword.operator.assignment.cidr"},4:{patterns:[{include:"#intNoExp"}]}},match:`(?x) (?:\\G|^|(?<!\\.)\\b)
(?!\\.)
((?:
	\\.?
	(?: 25[0-5]    # 250-255
	|   2[0-4]\\d  # 200-249
	|   1\\d\\d    # 100-199
	|   [1-9]?\\d  # 0-99
	)\\b
){4})

# CIDR notation: \u201C/[0-32]\u201D
((/)(3[0-2]|[12]?\\d)\\b)?

(?=$|\\s|(?!\\.)\\b)`,name:"constant.numeric.other.ip-address.v4"},ipv6:{captures:{0:{patterns:[{captures:{1:{name:"meta.zone-id"},2:{name:"keyword.operator.assignment.zone-id"},3:{name:"entity.other.zone-index"},4:{name:"meta.cidr-notation"},5:{name:"keyword.operator.assignment.cidr"},6:{patterns:[{include:"#intNoExp"}]}},match:"(?!$)((%)([^/%]+))?((/)([0-9]+))?$"},{include:"#colon"}]}},match:`(?mix) (?:\\G|^|(?<!\\.|:|\\w))
(?<dec>25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d){0}
(?<hex>[A-F0-9]{1,4}){0}
(?<v4>\\g<dec>(?:\\.\\g<dec>){3}){0}
(?<v6>\\g<hex>:){0}

# Address
(?: \\g<v6>{7} (?:                               \\g<hex>      |:)
|   \\g<v6>{6} (?:                   \\g<v4>|   :\\g<hex>      |:)
|   \\g<v6>{5} (?:                  :\\g<v4>|(?::\\g<hex>){1,2}|:)
|   \\g<v6>{4} (?:(?::\\g<hex>)?    :\\g<v4>|(?::\\g<hex>){1,3}|:)
|   \\g<v6>{3} (?:(?::\\g<hex>){0,2}:\\g<v4>|(?::\\g<hex>){1,4}|:)
|   \\g<v6>{2} (?:(?::\\g<hex>){0,3}:\\g<v4>|(?::\\g<hex>){1,5}|:)
|   \\g<v6>    (?:(?::\\g<hex>){0,4}:\\g<v4>|(?::\\g<hex>){1,6}|:)
|          (?::(?:(?::\\g<hex>){0,5}:\\g<v4>|(?::\\g<hex>){1,7}|:))
) (?:(?<!:)\\b|(?<=:)(?!\\w|:))

# Zone ID (RFC 4007): \u201C%ne0\u201D
(?:
	%
	(?!\\$)
	[^\\s();"/]+
)?

# CIDR notation: \u201C/[0-128]\u201D
(?:
	/
	(?: 12[0-8]   # 120-128
	|   1[01]\\d  # 100-119
	|   [1-9]?\\d # 0-99
	) \\b
)?

(?=$|\\s|[^.:%/\\s])`,name:"constant.numeric.other.ip-address.v6"},kolon:{match:":",name:"keyword.operator.assignment.key-value.colon"},mime:{captures:{1:{name:"entity.name.type.standard.${1:/downcase}.media-type"},2:{name:"entity.name.type.nonstandard.${2:/downcase}.media-type"},3:{name:"entity.name.type.extension.media-type"},4:{name:"punctuation.separator.slash.media-type"},5:{patterns:[{begin:"(?i)(?:^|\\G)(?:(([-a-z0-9_]+)(\\.))((?:[-a-z0-9_]+\\.)*+))?",beginCaptures:{1:{name:"entity.other.tree.${2:/downcase}.media-type"},3:{name:"punctuation.separator.dot.media-type"},4:{patterns:[{captures:{2:{name:"punctuation.separator.dot.media-type"}},match:"([^.]+)(\\.)",name:"entity.other.subtree.${1:/downcase}.media-type"}]}},contentName:"entity.name.subtype.media-type",end:"((\\+)[-a-z0-9_.]+)?$",endCaptures:{1:{name:"entity.other.suffix.media-type"},2:{name:"punctuation.separator.plus.media-type"}}}]},6:{name:"meta.parameters.media-type",patterns:[{begin:"\\s*((;))\\s*([^;=]+)\\s*(=)[ \\t]*",beginCaptures:{1:{name:"punctuation.delimiter.semicolon.media-type"},2:{name:"sublimelinter.gutter-mark"},3:{name:"variable.parameter.media-type"},4:{patterns:[{include:"#eql"}]}},end:"(?!\\G)",name:"meta.parameter.media-type",patterns:[{captures:{1:{name:"punctuation.definition.string.begin.media-type"},2:{patterns:[{match:'\\\\"',name:"constant.character.escape.quote.media-type"}]},3:{name:"punctuation.definition.string.end.media-type"}},match:'\\G(")((?:[^"\\\\]|\\\\")*+)(")',name:"string.quoted.double.media-type"},{match:"\\G[^;\\r\\n]+",name:"string.unquoted.parameter.media-type"}]}]}},match:`(?xi) (?<!-|\\.)\\b
(?: (application|audio|example|font|image|message|model|multipart|text|video)
|   (chemical|drawing|magnus-internal|paleovu|xgl|inode(?=/(?:blockdevice|chardevice|directory|door|fifo|mount-point|socket|symlink)))
|   (x-[-a-z0-9+_.]{1,125})
) (/) (?![.+])([-a-z0-9+_.]{1,127})(?<![.+])
\\b (?!-|\\.)
((?:
	# LHS: \u201C; name=\u201D
	\\s* ; \\s* [^;=]+ \\s* = [ \\t]*
	
	# RHS
	(?: "(?:[^"\\\\]|\\\\")*+" # Quoted value
	|   (?=\\S)[^;\\r\\n]+     # Unquoted value
	)?
)++)?`,name:"constant.other.media-type"},num:{patterns:[{include:"#float"},{include:"#int"}]},op:{patterns:[{include:"#opBitAssign"},{include:"#opMathAssign"},{include:"#opBit"},{include:"#opFix"},{include:"#opCmp"},{include:"#opLog"},{include:"#opMath"}]},opBit:{patterns:[{match:"\\^",name:"keyword.operator.bitwise.xor"},{match:"~",name:"keyword.operator.bitwise.not"},{match:"&",name:"keyword.operator.bitwise.and"},{match:"\\|",name:"keyword.operator.bitwise.or"},{match:"<<",name:"keyword.operator.bitwise.shift.left"},{match:">>>",name:"keyword.operator.bitwise.shift.right.unsigned"},{match:">>",name:"keyword.operator.bitwise.shift.right.signed"}]},opBitAssign:{patterns:[{match:"\\^=",name:"keyword.operator.assignment.bitwise.xor"},{match:"~=",name:"keyword.operator.assignment.bitwise.not"},{match:"&=",name:"keyword.operator.assignment.bitwise.and"},{match:"\\|=",name:"keyword.operator.assignment.bitwise.or"},{match:"<<=",name:"keyword.operator.assignment.bitwise.shift.left"},{match:">>>=",name:"keyword.operator.assignment.bitwise.shift.right.unsigned"},{match:">>=",name:"keyword.operator.assignment.bitwise.shift.right.signed"}]},opCmp:{patterns:[{match:"<=>",name:"keyword.operator.logical.comparison.starship.spaceship"},{match:"<=",name:"keyword.operator.logical.comparison.less-than-or-equal-to.lte"},{match:"<",name:"keyword.operator.logical.comparison.less-than.lt"},{match:">=",name:"keyword.operator.logical.comparison.greater-than-or-equal-to.gte"},{match:">",name:"keyword.operator.logical.comparison.greater-than.gt"},{match:"===",name:"keyword.operator.logical.comparison.equal-to.equals.equal.eql.eq.strict"},{match:"==",name:"keyword.operator.logical.comparison.equal-to.equals.equal.eql.eq"},{match:"!==",name:"keyword.operator.logical.comparison.not-equal-to.not-equal.unequal.neql.ne.strict"},{match:"!=",name:"keyword.operator.logical.comparison.not-equal-to.not-equal.unequal.neql.ne"}]},opFix:{patterns:[{match:"\\+{2}",name:"keyword.operator.increment"},{match:"-{2}",name:"keyword.operator.decrement"}]},opLog:{patterns:[{match:"!!",name:"keyword.operator.logical.boolean.cast"},{match:"!",name:"keyword.operator.logical.boolean.not.negation.negate"},{match:"&&",name:"keyword.operator.logical.boolean.and"},{match:"\\|{2}",name:"keyword.operator.logical.boolean.or"},{match:"\\?{2}",name:"keyword.operator.logical.boolean.or.nullish"}]},opMath:{patterns:[{match:"\\*{2}|\\^",name:"keyword.operator.arithmetic.exponentiation.exponent.exp.power"},{match:"\\+",name:"keyword.operator.arithmetic.addition.add.plus"},{match:"\\*",name:"keyword.operator.arithmetic.multiplication.multiply.times"},{match:"/",name:"keyword.operator.arithmetic.division.divide"},{match:"%",name:"keyword.operator.arithmetic.remainder.modulo.modulus.mod"},{match:"[-\u058A\u05BE\u1400\u1806\u2010-\u2015\u2E17\u2E1A\u2E3A\u2E3B\u2E40\u301C\u3030\u30A0\uFE31\uFE32\uFE58\uFE63\uFF0D]",name:"keyword.operator.arithmetic.subtraction.subtract.minus"}]},opMathAssign:{patterns:[{match:"\\*{2}=|\\^=",name:"keyword.operator.assignment.arithmetic.exponentiation.exponent.exp.power"},{match:"\\+=",name:"keyword.operator.assignment.arithmetic.addition.add.plus"},{match:"\\*=",name:"keyword.operator.assignment.arithmetic.multiplication.multiply.times"},{match:"/=",name:"keyword.operator.assignment.arithmetic.division.divide"},{match:"%=",name:"keyword.operator.assignment.arithmetic.remainder.modulo.modulus.mod"},{match:"[-\u058A\u05BE\u1400\u1806\u2010-\u2015\u2E17\u2E1A\u2E3A\u2E3B\u2E40\u301C\u3030\u30A0\uFE31\uFE32\uFE58\uFE63\uFF0D]=",name:"keyword.operator.assignment.arithmetic.subtraction.subtract.minus"}]},semi:{match:";",name:"punctuation.delimiter.separator.semicolon"},str:{patterns:[{include:"#strDouble"},{include:"#strSingle"}]},strDouble:{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin"}},end:'"|(?=$)',endCaptures:{0:{name:"punctuation.definition.string.end"}},name:"string.quoted.double",patterns:[{include:"#esc"}]},strSingle:{begin:"'",beginCaptures:{0:{name:"punctuation.definition.string.begin"}},end:"'|(?=$)",endCaptures:{0:{name:"punctuation.definition.string.end"}},name:"string.quoted.single",patterns:[{include:"#esc"}]},tab:{patterns:[{captures:{0:{patterns:[{match:"\\t",name:"punctuation.whitespace.leading.tab.hard-tab"}]}},match:"^\\t+"},{captures:{0:{patterns:[{match:"\\t",name:"punctuation.whitespace.trailing.tab.hard-tab"}]}},match:"\\t+$"},{match:"\\t",name:"punctuation.whitespace.tab.hard-tab"}]},url:{patterns:[{captures:{1:{name:"punctuation.definition.link.begin.url"},2:{name:"constant.other.reference.link.underline.$3.url"},4:{name:"punctuation.definition.link.end.url"}},match:`(?x)
("|'|\\b)
(
	# Not part of official URL schemes, included here for convenience
	(?: (?:jdbc|mvn|odbc|view-source) :)?

	# Common protocols/URI schemes
	( https?
	| s?ftp
	| ftps
	| file
	| wss?
	| (?:git|svn) (?:\\+(?:https?|ssh))?
	| ssh
	
	# Less common URI schemes
	| aaas?
	| acap
	| adiumxtra
	| admin
	| afp
	| app
	| atom
	| aurora
	| aw
	| beshare
	| bolo
	| cassandra
	| chrome(?:-extension)?
	| coaps?
	| cockroach
	| content
	| couchbase
	| crid
	| cvs
	| dict
	| dns
	| docker
	| ed2k
	| facetime
	| feed
	| finger
	| fish
	| gemini
	| github(?:-(?:mac|linux|windows))?
	| gizmoproject
	| gopher
	| go
	| hcp
	| imap
	| irc[6s]?
	| issue
	| keyparc
	| lastfm
	| ldaps?
	| man(?:-?page)?
	| maria(?:db)?
	| market
	| message
	| mms
	| modern-?sqlite
	| mongodb
	| ms-help
	| mssql
	| mumble
	| my?sql
	| netezza
	| nfs
	| ni
	| nntp
	| notes
	| oleodbc
	| oracle
	| payto
	| pgsql
	| pg
	| pop
	| postgres(?:ql)?
	| postgresql
	| presto(?:dbs?|s)
	| reload
	| resource
	| res
	| rmi
	| rsync
	| rtmf?p
	| rtmp
	| s3
	| saphana
	| secondlife
	| sgn
	| shttp
	| slack
	| smb
	| snmp
	| soldat
	| sqlite3?
	| sqlserver
	| steam
	| stratum\\+[a-z]+
	| stuns?
	| teamspeak
	| telnet
	| turns?
	| txmt
	| udp
	| unreal
	| ut2004
	| ventrilo
	| vnc
	| wais
	| web\\+[a-z]+
	| webcal
	| wtai
	| wyciwyg
	| xmpp
	| xri
	| z39\\.50[rs]
	| zoommtg
	
	# User-defined/arbitrary URI scheme starting with \`x-\`
	| x(?:-[a-z][a-z0-9]*)++
	) ://
	
	# Path specifier
	(?:
		(?! \\#\\w*\\#)
		(?: [-:\\@\\w.,~%+_/?=&\\#;|!])
	)+
	
	# Don't include trailing punctuation
	(?<![-.,?:\\#;])
)
(\\1)`},{captures:{1:{name:"punctuation.definition.link.begin.url"},2:{name:"constant.other.reference.link.underline.mailto.url"},3:{name:"punctuation.separator.delimiter.scheme.url"},4:{name:"punctuation.definition.link.end.url"}},match:`(?x)
("|'|\\b)
(
	mailto (:)
	(?:
		(?! \\#\\w*\\#)
		(?: [-:@\\w.,~%+_/?=&\\#;|!])
	)+
	(?<![-.,?:\\#;])
)
(\\1)`}]},version:{captures:{1:{name:"punctuation.definition.version-string.begin"},10:{name:"punctuation.delimiter.separator.plus"},11:{name:"meta.build-metadata",patterns:[{include:"#dot"}]},12:{name:"punctuation.definition.version-string.end"},2:{name:"punctuation.definition.version-prefix"},3:{name:"meta.major.release-number"},4:{patterns:[{include:"#dot"}]},5:{name:"meta.minor.release-number"},6:{patterns:[{include:"#dot"}]},7:{name:"meta.patch.release-number"},8:{patterns:[{include:"#dash"}]},9:{name:"meta.prerelease.release-number",patterns:[{include:"#dot"}]}},match:`(?x)
("|'|\\b)
([vV]?)
(0 | [1-9]\\d*) (\\.)
(0 | [1-9]\\d*) (\\.)
(0 | [1-9]\\d*)
(?:
	(-)
	(
		(?: 0
		| [1-9]\\d*
		| \\d*[a-zA-Z-][0-9a-zA-Z-]*
		)
		
		(?:
			\\.
			(?: 0
			| [1-9]\\d*
			| \\d*[a-zA-Z-][0-9a-zA-Z-]*
			)
		)*
	)
)?
(?:
	(\\+)
	(
		[0-9a-zA-Z-]+
		(?:\\.[0-9a-zA-Z-]+)*
	)
)?
(\\1)`,name:"constant.other.version-string"}},scopeName:"etc"},t=e;export{t as default};
