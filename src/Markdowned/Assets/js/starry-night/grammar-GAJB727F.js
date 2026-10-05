import"./grammar-MHCU5XT2.js";var n={extensions:[".zil",".mud"],names:["zil"],patterns:[{include:"#expressions"}],repository:{argspec:{begin:"\\(",beginCaptures:{0:{name:"punctuation.definition.list.parameters.begin.zil"}},end:"\\)",endCaptures:{0:{name:"punctuation.definition.list.parameters.end.zil"}},name:"meta.parameters.zil",patterns:[{match:`(?x)
# optional quote
(?:'\\s*)?
# atom
(?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
(?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+`,name:"variable.parameter.local.symbol.atom.zil"},{begin:`(?x)
(\\()
\\s*
# optional quote
(?:'\\s*)?
# atom
((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
 (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)`,beginCaptures:{1:{name:"punctuation.definition.list.parameter.begin.zil"},2:{name:"variable.parameter.local.symbol.atom.zil"}},end:"\\)",endCaptures:{0:{name:"punctuation.definition.list.parameter.end.zil"}},name:"meta.binding.zil",patterns:[{include:"#expressions"}]},{match:'"(?:AUX|EXTRA)"',name:"punctuation.separator.arguments.aux.zil keyword.separator.arguments.aux.zil"},{match:'"(?:OPT|OPTIONAL)"',name:"punctuation.separator.arguments.opt.zil keyword.separator.arguments.opt.zil"},{match:'"(?:ARGS|TUPLE)"',name:"punctuation.separator.arguments.varargs.zil keyword.separator.arguments.varargs.zil"},{match:'"(?:NAME|BIND)"',name:"punctuation.separator.arguments.misc.zil keyword.separator.arguments.misc.zil"},{include:"#expressions"}]},atom:{match:`(?x)
# atom can start with anything escaped, or any non-delimiter
(?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
# and continue with any of the above as well as '!' and '.'
(?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+`,name:"meta.symbol.atom.zil"},binary_num:{captures:{1:{name:"punctuation.definition.constant.numeric.binary.zil"}},match:`(#)\\s*0*2\\s+[01]+(?![^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"constant.numeric.binary.zil"},char:{match:"!\\\\.",name:"constant.character.zil"},comment:{applyEndPatternLast:!0,begin:";",beginCaptures:{0:{name:"punctuation.definition.comment.prefix.zil"}},end:"(?<!\\G)",name:"comment.block.zil",patterns:[{include:"#unstyled_expressions"}]},constants:{patterns:[{include:"#decimal_num"},{include:"#octal_num"},{include:"#binary_num"},{include:"#string"},{include:"#char"},{include:"#else"},{include:"#false"},{include:"#true"},{include:"#atom"}]},decimal_num:{match:`-?([0-9]+)(?![^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"constant.numeric.decimal.zil"},else:{match:"(?<=\\()(?:ELSE|T)\\b",name:"keyword.control.else.zil"},expressions:{patterns:[{include:"#constants"},{include:"#structures"},{include:"#prefixes"},{include:"#invalid"}]},false:{match:"<\\s*>",name:"constant.language.boolean.false.zil"},form:{begin:"<",beginCaptures:{0:{name:"punctuation.definition.form.begin.zil"}},end:"!?>",endCaptures:{0:{name:"punctuation.definition.form.end.zil"}},name:"meta.structure.form.zil",patterns:[{include:"#special_form_body"},{include:"#expressions"}]},gval:{applyEndPatternLast:!0,begin:"(,)\\s*",beginCaptures:{1:{name:"punctuation.definition.variable.global.prefix.zil"}},end:"(?<!\\G)",name:"variable.other.global.zil",patterns:[{include:"#unstyled_expressions"}]},invalid:{patterns:[{match:"!.",name:"invalid.illegal.zil"}]},list:{begin:"!?\\(",beginCaptures:{0:{name:"punctuation.definition.list.begin.zil"}},end:"!?\\)",endCaptures:{0:{name:"punctuation.definition.list.end.zil"}},name:"meta.structure.list.zil",patterns:[{include:"#expressions"}]},lval:{applyEndPatternLast:!0,begin:"(\\.)\\s*",beginCaptures:{1:{name:"punctuation.definition.variable.local.prefix.zil"}},end:"(?<!\\G)",name:"variable.other.local.zil",patterns:[{include:"#unstyled_expressions"}]},macro:{applyEndPatternLast:!0,begin:"(%)|(%%)",beginCaptures:{1:{name:"punctuation.definition.macro.single.prefix.zil"},2:{name:"punctuation.definition.macro.double.prefix.zil"}},end:"(?<!\\G)",name:"meta.macro.zil",patterns:[{include:"#unstyled_expressions"}]},octal_num:{captures:{1:{name:"punctuation.definition.constant.numeric.octal.zil"},3:{name:"punctuation.definition.constant.numeric.octal.zil"}},match:`(\\*)([0-7]+)(\\*)(?![^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"constant.numeric.octal.zil"},prefixes:{patterns:[{include:"#comment"},{include:"#macro"},{include:"#lval"},{include:"#gval"},{include:"#quote"},{include:"#segment"}]},property:{begin:`(?xi)
\\( \\s*
(?: (IN|LOC|DESC|SYNONYM|ADJECTIVE|FLAGS|
     GLOBAL|GENERIC|ACTION|DESCFCN|CONTFCN|LDESC|FDESC|
     NORTH|SOUTH|EAST|WEST|OUT|UP|DOWN|NW|SW|NE|SE)
|   ((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
     (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)
)
\\b`,beginCaptures:{1:{name:"storage.property.${1:/downcase}.zil"},2:{name:"meta.object-literal.key.zil"}},end:"\\)",name:"meta.property.zil",patterns:[{include:"$self"}]},quote:{applyEndPatternLast:!0,begin:"'",beginCaptures:{0:{name:"punctuation.definition.quote.prefix.zil"}},end:"(?<!\\G)",name:"meta.quoted-expression.zil",patterns:[{include:"#unstyled_expressions"}]},segment:{applyEndPatternLast:!0,begin:"!(?=[.,<])",beginCaptures:{0:{name:"punctuation.definition.segment.prefix.zil"}},end:"(?<!\\G)",name:"meta.structure.segment.zil",patterns:[{include:"#unstyled_expressions"}]},skip_ws:{match:"\\s+"},special_form_body:{patterns:[{match:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
(\\+|-|\\*|/|MOD|MIN|MAX|OR\\?|AND\\?)
(?!\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"keyword.operator.arithmetic.${1:/downcase}.zil"},{match:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
(BAND|BOR|ANDB|ORB|LSH|XORB|EQVB)
(?!\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"keyword.operator.bitwise.${1:/downcase}.zil"},{match:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
(==?|N==?|L=?|G=?|[01TF])\\?
(?!\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"keyword.operator.comparison.${1:/downcase}.zil"},{match:`(?xi) \\s*
(?<=<) \\s* (?:FORM\\s+)?
(COND|BIND|PROG|REPEAT|DO|MAP[FR]|MAP-(?:CONTENTS|DIRECTIONS)|
 AGAIN|RETURN|RTRUE|RFALSE|CATCH|THROW|EVAL|AND|OR|NOT)
(?!\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"keyword.control.${1:/downcase}.zil"},{match:`(?xi) \\s*
(?<=<) \\s* (?:FORM\\s+)?
(TELL(?:-TOKENS)?|ADD-TELL-TOKENS|CRLF|PRINT[INR]?|PRIN[C1])
(?!\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"keyword.output.${1:/downcase}.zil"},{match:`(?xi) \\s*
(?<=<) \\s* (?:FORM\\s+)?
(FSET\\??|FCLEAR|MOVE|REMOVE|IN\\?|FIRST\\?|NEXT\\?|
 PUTP|GETP|PROPDEF|GETPT|PTSIZE|INTBL\\?|
 P?L?TABLE|ITABLE|GETB?|GET/B|PUTB?|PUT/B|ZGET|ZPUT|
 VOC|SYNONYM|(?:VERB|PREP|ADJ|DIR|BIT)-SYNONYM|DIRECTIONS|BUZZ)
(?!\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"keyword.zmodel.${1:/downcase}.zil"},{match:`(?xi) \\s*
(?<=<) \\s* (?:FORM\\s+)?
(INSERT-FILE|PACKAGE|ENDPACKAGE|USE|ENTRY|RENTRY|VERSION|
 COMPILATION-FLAG(?:-DEFAULT)?|REPLACE-DEFINITION|DELAY-DEFINITION|DEFAULT-DEFINITION|
 IF-(?:[A-Z0-9][-A-Z0-9]+))
(?!\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`,name:"keyword.meta.${1:/downcase}.zil"},{begin:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
# function type (1)
(DEFINE|DEFINE20|DEFMAC|ROUTINE)
\\s+
# function name atom (2)
((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
 (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)
\\s*
# optional activation atom (3)
(?:
    (?<=\\s)
    ((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
     (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)
     \\s*
)?
# followed by paren starting arg spec
(?=\\()`,beginCaptures:{1:{name:"keyword.definition.function.${1:/downcase}.zil"},2:{name:"entity.name.function.zil"},3:{name:"entity.name.variable.local.activation-atom.zil"}},end:"(?<=\\))|(?=\\>)",name:"meta.function.zil",patterns:[{include:"#argspec"}]},{begin:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
# object type (1)
(OBJECT|ROOM)
\\s+
# object name (2)
((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
 (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)
\\s*`,beginCaptures:{1:{name:"keyword.definition.object.${1:/downcase}.zil"},2:{name:"entity.name.object.zil"}},end:"(?=\\>)",name:"meta.object.zil",patterns:[{include:"#property"}]},{captures:{1:{name:"keyword.definition.global.${1:/downcase}.zil"},2:{name:"entity.name.variable.global.zil variable.global.zil"}},match:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
# global type (1)
(SETG|CONSTANT|GLOBAL|GASSIGNED\\?|GUNASSIGN)
\\s+
# global name atom (2)
((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
 (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)?`},{captures:{1:{name:"keyword.definition.local.${1:/downcase}.zil"},2:{name:"entity.name.variable.local.zil variable.local.zil"}},match:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
# local type?
(SET|ASSIGNED\\?|UNASSIGN)
\\s+
# local name atom (2)
((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
 (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)?`},{captures:{1:{name:"keyword.type.${1:/downcase}.zil"}},match:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
(CHTYPE|TYPE\\??|PRIMTYPE)
(?!\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`},{captures:{1:{name:"keyword.definition.type.${1:/downcase}.zil"},2:{name:"entity.name.type.zil"}},match:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
# keyword (1)
(NEWTYPE|DEFSTRUCT|APPLYTYPE|EVALTYPE|PRINTTYPE|TYPEPRIM)
\\s+
# type name atom (2)
((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
 (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)?
(?!\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`},{captures:{1:{name:"keyword.definition.vocab.syntax.zil"},10:{name:"entity.name.function.preaction.zil"},11:{name:"entity.name.verb.zil"},2:{name:"entity.name.verb.zil"},3:{name:"entity.name.preposition.zil"},4:{name:"keyword.definition.vocab.object.zil"},6:{name:"entity.name.preposition.zil"},7:{name:"keyword.definition.vocab.object.zil"},9:{name:"entity.name.function.action.zil"}},match:`(?xi)
(?<=<) \\s* (?:FORM\\s+)?
(SYNTAX)
\\s+
# verb word
((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
 (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)
# first object
(?:
    (?!\\s*=\\b)
    # prep 1
    (?:\\s+ (?!OBJECT\\b)(\\S+))?
    # obj 1
    \\s+ (OBJECT)
    # flags/search options 1
    (\\s* \\( [^)]* \\) )*
)?+
# second object
(?:
    (?!\\s*=\\b)
    # prep 2
    (?:\\s+ (?!OBJECT\\b)(\\S+))?
    # obj 2
    \\s+ (OBJECT)
    # flags/search options 2
    (\\s* \\( [^)]* \\) )*
)?+
# handlers
(?:
    \\s+ =
    # action
    \\s+
    ((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
     (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)
    # preaction
    (?:
        \\s+
        ((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
         (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)
    )?+
    # name
    (?:
        \\s+
        ((?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
         (?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+)
    )?+
)?+`}]},string:{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin.zil"}},end:'"',endCaptures:{0:{name:"punctuation.definition.string.end.zil"}},name:"string.quoted.double.zil",patterns:[{match:"\\\\.",name:"constant.character.escape.zil"}]},structures:{patterns:[{include:"#list"},{include:"#form"},{include:"#vector"},{include:"#uvector"},{include:"#segment"}]},true:{match:"\\bT\\b",name:"constant.language.boolean.true.zil"},unstyled_atom:{match:`(?x)
# atom can start with anything escaped, or any non-delimiter
(?:\\\\.|[^!. \\t-\\r,#':;%()\\[\\]<>\\{\\}"])
# and continue with any of the above as well as '!' and '.'
(?:\\\\.|[^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])*+`},unstyled_char:{match:"!\\\\."},unstyled_constants:{patterns:[{include:"#unstyled_numeric"},{include:"#unstyled_string"},{include:"#unstyled_char"},{include:"#unstyled_atom"}]},unstyled_expressions:{patterns:[{include:"#unstyled_constants"},{include:"#unstyled_structures"},{include:"#unstyled_prefixes"},{include:"#invalid"}]},unstyled_form:{begin:"<",end:"!?>",patterns:[{include:"#unstyled_expressions"}]},unstyled_gval:{applyEndPatternLast:!0,begin:"(,)\\s*",end:"(?<!\\G)",patterns:[{include:"#unstyled_expressions"}]},unstyled_list:{begin:"!?\\(",end:"!?\\)",patterns:[{include:"#unstyled_expressions"}]},unstyled_lval:{applyEndPatternLast:!0,begin:"\\.",end:"(?<!\\G)",patterns:[{include:"#unstyled_expressions"}]},unstyled_numeric:{match:`(?:\\b-?[0-9]+\\b|\\*[0-7]+\\*|#\\s*0*2\\s+[01]+)(?![^ \\t-\\r,#':;%()\\[\\]<>\\{\\}"])`},unstyled_prefixes:{patterns:[{include:"#comment"},{include:"#macro"},{include:"#unstyled_lval"},{include:"#unstyled_gval"},{include:"#unstyled_quote"},{include:"#unstyled_segment"}]},unstyled_quote:{applyEndPatternLast:!0,begin:"'",end:"(?<!\\G)",patterns:[{include:"#unstyled_expressions"}]},unstyled_segment:{applyEndPatternLast:!0,begin:"!(?=[.,<])",end:"(?<!\\G)",patterns:[{include:"#unstyled_expressions"}]},unstyled_string:{begin:'"',end:'"',patterns:[{match:"\\\\."}]},unstyled_structures:{patterns:[{include:"#unstyled_list"},{include:"#unstyled_form"},{include:"#unstyled_vector"},{include:"#unstyled_uvector"},{include:"#unstyled_segment"}]},unstyled_uvector:{begin:"!\\[",end:"!?\\]",patterns:[{include:"#unstyled_expressions"}]},unstyled_vector:{begin:"\\[",end:"!?\\]",patterns:[{include:"#unstyled_expressions"}]},uvector:{begin:"!\\[",beginCaptures:{0:{name:"punctuation.definition.array.uvector.begin.zil"}},end:"!?\\]",endCaptures:{0:{name:"punctuation.definition.array.uvector.end.zil"}},name:"meta.structure.array.uvector.zil",patterns:[{include:"#expressions"}]},vector:{begin:"\\[",beginCaptures:{0:{name:"punctuation.definition.array.vector.begin.zil"}},end:"!?\\]",endCaptures:{0:{name:"punctuation.definition.array.vector.end.zil"}},name:"meta.structure.array.vector.zil",patterns:[{include:"#expressions"}]}},scopeName:"source.zil"},e=n;export{e as default};
