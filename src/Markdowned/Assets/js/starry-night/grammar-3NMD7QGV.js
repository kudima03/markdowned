import"./grammar-MHCU5XT2.js";var n={dependencies:["source.lex.regexp"],extensions:[".ebnf"],names:["ebnf"],patterns:[{include:"#main"}],repository:{brackets:{patterns:[{match:"\\[",name:"punctuation.definition.square.bracket.begin.ebnf"},{match:"\\]",name:"punctuation.definition.square.bracket.end.ebnf"},{match:"{",name:"punctuation.definition.curly.bracket.begin.ebnf"},{match:"}",name:"punctuation.definition.curly.bracket.end.ebnf"},{match:"\\(",name:"punctuation.definition.round.bracket.begin.ebnf"},{match:"\\)",name:"punctuation.definition.round.bracket.end.ebnf"}]},comment:{patterns:[{begin:"\\(\\*",beginCaptures:{0:{name:"punctuation.definition.comment.begin.ebnf"}},end:"\\*\\)",endCaptures:{0:{name:"punctuation.definition.comment.end.ebnf"}},name:"comment.block.iso14977.ebnf"},{begin:"^[ \\t]*((/\\*))",beginCaptures:{1:{name:"comment.block.w3c.ebnf"},2:{name:"punctuation.definition.comment.begin.ebnf"}},end:"\\*/",endCaptures:{0:{name:"punctuation.definition.comment.end.ebnf"}},name:"comment.block.w3c.ebnf"}]},lhs:{patterns:[{captures:{1:{name:"entity.name.rule.identifier.ebnf"}},match:`(?x)
(?:    \\s++
|      ^|\\G
| (?=  ^|\\G    )
| (?<= ;|\\*\\) )
)

# Exclude leading whitespace
\\s*

([A-Za-z][A-Za-z0-9_-]*+)`,name:"meta.lhs.ebnf"},{begin:`(?x)
(?:    \\s++
|      ^|\\G
| (?=  ^|\\G  )
| (?<= \\*\\) )
)

# Exclude leading whitespace
\\s*

# Check for at least one \u201Cinvalid\u201D character
(?=
	# Starts with a digit
	[0-9]
	|
	
	# Contains at least one non-\u201Cword\u201D character
	[A-Za-z0-9_-]*  # Skip any legal characters
	(?: [^:;=()]    # Don't swallow symbols for comments, terminators, or assignments
	|   \\((?!\\*)  # Permit open brackets if they don't introduce a comment
	)
)`,contentName:"entity.name.rule.identifier.non-standard.ebnf",end:`(?x)
# Exclude trailing whitespace
\\s*

# Stop before an...
(?= :*=      # Assignment operator separating \`#lhs\` from \`#rhs\`
|   ;        # Unexpected terminator
|   \\(\\*   # Embedded comment
)`,name:"meta.lhs.non-standard.ebnf",patterns:[{include:"#comment"}]},{include:"#comment"}]},main:{patterns:[{include:"#comment"},{include:"#semicolon"},{include:"#lhs"},{include:"#rhs"},{include:"#special"}]},rhs:{begin:"(::=)|(:=)|(=)",beginCaptures:{1:{name:"keyword.operator.assignment.non-standard.double-colon.ebnf"},2:{name:"keyword.operator.assignment.non-standard.single-colon.ebnf"},3:{name:"keyword.operator.assignment.ebnf"}},end:"(?=;|^\\s*(?:<?[A-Za-z][A-Za-z0-9_-]*>?\\s*)?:*=)",name:"meta.rhs.ebnf",patterns:[{include:"#rhs-innards"}]},"rhs-innards":{patterns:[{match:",",name:"punctuation.delimiter.comma.ebnf"},{match:"\\|",name:"keyword.operator.logical.or.alternation.pipe.ebnf"},{match:"-",name:"keyword.operator.logical.minus.hyphen.exception.ebnf"},{match:"\\*",name:"keyword.operator.logical.repetition.asterisk.star.ebnf"},{include:"#special"},{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin.ebnf"}},end:'"(?!")|(?=^\\s*+\\S+?\\s+::{0,2}=\\s|\\s+\\|)',endCaptures:{0:{name:"punctuation.definition.string.end.ebnf"}},name:"string.quoted.double.ebnf"},{begin:"'",beginCaptures:{0:{name:"punctuation.definition.string.begin.ebnf"}},end:"'(?!')|(?=^\\s*+\\S+?\\s+::{0,2}=\\s|\\s+\\|)",endCaptures:{0:{name:"punctuation.definition.string.end.ebnf"}},name:"string.quoted.single.ebnf"},{include:"#brackets"},{include:"#comment"},{match:"\\b(?<!-)[A-Za-z][A-Za-z0-9_-]*",name:"variable.parameter.argument.identifier.reference.ebnf"},{match:"!",name:"keyword.operator.logical.not.negation.non-standard.ebnf"},{include:"source.lex.regexp#quantifier"}]},semicolon:{match:";",name:"punctuation.terminator.statement.ebnf"},special:{captures:{1:{name:"keyword.operator.pragma.begin.ebnf"},2:{name:"support.constant.language.pragma.ebnf"},3:{name:"keyword.operator.pragma.end.ebnf"}},match:"(?<=\\s|^)(\\?)(.+?)(?<=\\s)(\\?)(?=[,;]?(?:$|\\s))",name:"meta.pragma.directive.special.iso14977.ebnf"}},scopeName:"source.ebnf"},e=n;export{e as default};
