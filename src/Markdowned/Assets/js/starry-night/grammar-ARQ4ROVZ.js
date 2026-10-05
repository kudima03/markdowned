import"./grammar-MHCU5XT2.js";var e={dependencies:["source.mermaid"],extensions:[],names:[],patterns:[{include:"#main"}],repository:{attribute:{captures:{1:{name:"storage.type.attribute.mermaid"},2:{name:"entity.name.attribute.mermaid"},3:{name:"constant.language.other.key-type.mermaid"},4:{patterns:[{include:"#string"}]}},match:`(?x)
((?=[a-zA-Z])[-\\w]+) # Attribute type
\\s+
((?=[a-zA-Z])[-\\w]+) # Attribute name
(?:\\s+ (PK|FK))?     # Primary/foreign key
(?:\\s* ("[^"]*"))?   # Comment`,name:"meta.attribute.mermaid"},attributes:{begin:"((?=[a-zA-Z])[-\\w]+)\\s*({)",beginCaptures:{1:{name:"entity.name.assignee.mermaid"},2:{patterns:[{include:"source.mermaid#brace"}]}},end:"}",endCaptures:{0:{patterns:[{include:"source.mermaid#brace"}]}},name:"meta.attributes.mermaid",patterns:[{include:"#attribute"},{include:"source.mermaid#a11y"},{include:"source.mermaid#directive"},{include:"source.mermaid#comment"}]},main:{patterns:[{include:"source.mermaid#a11y"},{include:"source.mermaid#directive"},{include:"source.mermaid#comment"},{include:"#attributes"},{include:"#relationship"}]},relationship:{captures:{1:{name:"entity.name.operand.first.mermaid"},2:{name:"keyword.operator.cardinality.mermaid"},3:{name:"entity.name.operand.second.mermaid"},4:{patterns:[{include:"source.mermaid#colon"}]},5:{patterns:[{include:"#string"}]},6:{name:"string.unquoted.label-text.mermaid"},7:{name:"invalid.illegal.unexpected-characters.mermaid"}},match:`(?x)
((?=[a-zA-Z])[-\\w]+) # Entity 1

(?:
	\\s*
	# Cardinality configuration thingie
	(
		# Entity 1's cardinality
		(?: \\|o   # Zero or one
		|   \\|\\| # Exactly one
		|   }o     # Zero or more
		|   }\\|   # One or more
		)
		
		# Stroke style
		(?: --     # Solid
		| \\.\\.   # Dashed
		)
		
		# Entity 2's cardinality
		(?: o\\|   # Zero or one
		| \\|\\|   # Exactly one
		| o{       # Zero or more
		| \\|{     # One or more
		)
	)
	\\s*
	((?=[a-zA-Z])[-\\w]+) # Entity 2
	
	# Relationship label
	\\s* (:) \\s*
	(?: ("[^"]*")             # Quoted
	|   ((?=[a-zA-Z])[-\\w]+) # Unquoted
	|   ((?:[^\\s%]|%(?!%))+) # Invalid
	)?
)?`,name:"meta.relationship.mermaid"},string:{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin.mermaid"}},end:'"',endCaptures:{0:{name:"punctuation.definition.string.end.mermaid"}},name:"string.quoted.double.label-text.mermaid",patterns:[{include:"source.mermaid#entity"}]}},scopeName:"source.mermaid.er-diagram"},t=e;export{t as default};
