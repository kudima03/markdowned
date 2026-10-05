import"./grammar-MHCU5XT2.js";var e={dependencies:["text.roff"],extensions:[],names:[],patterns:[{include:"#data"},{include:"#tags"}],repository:{data:{begin:"^\\s*((?:sun)?gremlinfile)(?=\\s|$).*",beginCaptures:{0:{name:"meta.file.start.gremlin"},1:{name:"keyword.control.flow.begin-file.gremlin"}},contentName:"meta.file.body.gremlin",end:"^\\s*(-1)\\s*$",endCaptures:{0:{name:"meta.file.end.gremlin"},1:{name:"comment.line.ignored.end-of-file.gremlin"}},patterns:[{match:`(?x)\\b
(ARC|BEZIER|BOTCENT|BOTLEFT|BOTRIGHT|BSPLINE|CENTCENT|CENTLEFT
|CENTRIGHT|CURVE|POLYGON|TOPCENT|TOPLEFT|TOPRIGHT|VECTOR)\\b`,name:"keyword.operator.element-specification.sun.gremlin"},{captures:{1:{name:"keyword.operator.element-brush.gremlin"},2:{name:"keyword.operator.element-size.gremlin"}},match:"^\\s*([0-6])\\s+([0-9]+)\\s*$"},{match:"^\\s*(?:\\*|-1.0+\\s+-1.0+)\\s*$",name:"comment.line.ignored.end-point-list.gremlin"},{match:"^\\s*[0-9]+(?:\\s*$|\\s+(?=\\d))",name:"keyword.operator.element-specification.aed.gremlin"},{captures:{1:{name:"meta.element-text.gremlin"},10:{name:"keyword.operator.character-count.2.gremlin"},11:{name:"string.unquoted.gremlin"},12:{name:"keyword.operator.character-count.1.gremlin"},13:{name:"string.unquoted.gremlin"},14:{name:"keyword.operator.character-count.gremlin"},15:{name:"string.unquoted.gremlin"},2:{name:"keyword.operator.character-count.6.gremlin"},3:{name:"string.unquoted.gremlin"},4:{name:"keyword.operator.character-count.5.gremlin"},5:{name:"string.unquoted.gremlin"},6:{name:"keyword.operator.character-count.4.gremlin"},7:{name:"string.unquoted.gremlin"},8:{name:"keyword.operator.character-count.3.gremlin"},9:{name:"string.unquoted.gremlin"}},match:`(?x) ^\\s*
( (6) \\s+ (\\S.{5})
| (5) \\s+ (\\S.{4})
| (4) \\s+ (\\S.{3})
| (3) \\s+ (\\S.{2})
| (2) \\s+ (\\S.{1})
| (1) \\s+ ([7-9\\D])
| ((?!0)\\d+) \\s+ (\\S.*)
) \\s* $`},{match:"\\d+(?:\\.\\d+)?",name:"constant.numeric.decimal.gremlin"}]},grn:{patterns:[{captures:{1:{name:"keyword.operator.point-scale.grn.roff"},2:{name:"constant.language.boolean.grn.roff"}},match:`(?ix) ^\\s*
(pointscale|pointscal|pointsca|pointsc|points
|point|poin|poi|po|p) (?:\\s+(on|off))?
(?=\\s|$) `,name:"meta.directive.preprocessor.grn.roff"},{captures:{1:{name:"keyword.control.directive.include.grn.roff"},2:{name:"string.unquoted.filename.grn.roff"}},match:"(?i)^\\s*(file|fil|fi|f)\\s+(\\S.*)",name:"meta.directive.preprocessor.grn.roff"},{match:`(?ix) ^\\s*
( [1-4] (?=\\s+\\S)
| roman|roma|rom|ro|r
| italics|italic|itali|ital|ita|it|i
| bold|bol|bo|b
| special|specia|speci|spec|spe|sp
| stipple|stippl|stipp|stip|sti|st|l
| scale|scal|sca|sc|x
| narrow|narro|narr|nar|na
| medium|mediu|medi|med|me
| thick|thic|thi|th|t
| default|defaul|defau|defa|def|de|d
| width|widt|wid|wi|w
| height|heigh|heig|hei|he|h
) (?=\\s|$)`,name:"keyword.operator.directive.preprocessor.grn.roff"},{include:"text.roff#params"}]},tags:{begin:`^([.'])[ \\t]*(GS)(?=$|\\s|\\\\E?["#])(.*)$`,beginCaptures:{0:{name:"meta.function.begin.gremlin.macro.roff"},1:{name:"punctuation.definition.macro.roff"},2:{name:"entity.function.name.roff"},3:{patterns:[{match:"(?:^|\\G)\\s*([LIC])\\b",name:"constant.language.alignment-mode.grn.roff"},{include:"text.roff#escapes"}]}},end:`^([.'])[ \\t]*(GE|GF)(?=$|\\s|\\\\E?["#])`,endCaptures:{0:{name:"meta.function.end.gremlin.macro.roff"},1:{name:"punctuation.definition.macro.roff"},2:{name:"entity.name.function.roff"}},patterns:[{begin:"\\A\\s*((?:sun)?gremlinfile)(?=\\s|$)",end:"(?=A)B",patterns:[{include:"#data"}]},{include:"#grn"}]}},scopeName:"source.gremlin"},n=e;export{n as default};
