import"./grammar-MHCU5XT2.js";var e={dependencies:["etc"],extensions:[],names:["option-list","opts","ackrc"],patterns:[{include:"#main"}],repository:{escape:{patterns:[{include:"etc#esc"},{captures:{1:{name:"punctuation.definition.character.percentage.opts"}},match:"(%)[A-Fa-f0-9]{2}",name:"constant.character.percent.url-encoded.opts"}]},main:{patterns:[{include:"etc#comment"},{include:"#option"},{include:"#escape"}]},option:{patterns:[{begin:"((--?)[^-\\s=][^\\s=]*)",beginCaptures:{1:{name:"entity.name.option.opts"},2:{name:"punctuation.definition.option.name.dash.opts"}},end:"(?!\\G)(?=\\$|\\S)",name:"meta.option.opts",patterns:[{captures:{1:{name:"string.regexp.opts",patterns:[{include:"source.regexp"}]}},match:`(?xi)
(?<= # HACK: Fixed-width look-behinds enforced by Oniguruma
	\\w[-_]pattern \\G
	| reg[-_]exp   \\G
	| regexp       \\G
	| reg[-_]ex    \\G
	| regex        \\G
) \\s+ (\\S+)`},{captures:{1:{patterns:[{include:"etc#eql"}]},2:{patterns:[{include:"#value"}]}},match:"\\G(=)(\\S*)"},{captures:{1:{patterns:[{include:"#value"}]}},match:"\\G\\s+(?!#|-)(\\S+)"}]}]},value:{patterns:[{include:"etc"},{include:"etc#bareword"}]}},scopeName:"source.opts"},t=e;export{t as default};
