import"./grammar-MHCU5XT2.js";var t={extensions:[".rdoc"],names:["rdoc"],patterns:[{captures:{1:{name:"punctuation.definition.item.text"}},match:"^\\s*(\u2022).*$\\n?",name:"meta.bullet-point.strong.text"},{captures:{1:{name:"punctuation.definition.item.text"}},match:"^\\s*(\xB7).*$\\n?",name:"meta.bullet-point.light.text"},{captures:{1:{name:"punctuation.definition.item.text"}},match:"^\\s*(\\*).*$\\n?",name:"meta.bullet-point.star.text"},{begin:"^([ \\t]*)(?=\\S)",contentName:"meta.paragraph.text",end:"^(?!\\1(?=\\S))",patterns:[{match:`(?x)
						( (https?|s?ftp|ftps|file|smb|afp|nfs|(x-)?man|gopher|txmt)://|mailto:)
						[-:@a-zA-Z0-9_.,~%+/?=&#]+(?<![.,?:])
					`,name:"markup.underline.link.text"}]}],scopeName:"text.rdoc"},e=t;export{e as default};
