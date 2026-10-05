import"./grammar-MHCU5XT2.js";var t={dependencies:["source.fortran"],extensions:[".f90",".f03",".f08",".f95"],names:["fortran-free-form"],patterns:[{include:"source.fortran"},{begin:`(?x:					# extended mode
					^
					\\s*					# start of line and possibly some space
					(?i:(interface))		# 1: word interface
					\\s+					# some space
					(?i:(operator|assignment))		# 2: the words operator or assignment
					\\(					# opening parenthesis
					((\\.[a-zA-Z0-9_]+\\.)|[\\+\\-\\=\\/\\*]+)	# 3: an operator
					
					\\)					# closing parenthesis
					)`,beginCaptures:{1:{name:"storage.type.function.fortran"},2:{name:"storage.type.fortran"},3:{name:"keyword.operator.fortran"}},end:`(?xi)
				(end)(?=\\s|\\z)						# 1: the word end
				(?:
					\\s+
					(interface)						# 2: possibly interface
				)?
			`,endCaptures:{1:{name:"keyword.other.fortran"},2:{name:"storage.type.function.fortran"}},name:"meta.function.interface.operator.fortran.modern",patterns:[{include:"$self"}]},{begin:`(?x:					# extended mode
					^
					\\s*					# start of line and possibly some space
					(?i:(interface))		# 1: word interface
					\\s+					# some space
					([A-Za-z_][A-Za-z0-9_]*)	# 1: name
					)`,beginCaptures:{1:{name:"storage.type.function.fortran"},2:{name:"entity.name.function.fortran"}},end:`(?xi)
				(end)(?=\\s|\\z)						# 1: the word end
				(?:
					\\s+
					(interface)						# 2: possibly interface
				)?
			`,endCaptures:{1:{name:"keyword.other.fortran"},2:{name:"storage.type.function.fortran"}},name:"meta.function.interface.fortran.modern",patterns:[{include:"$self"}]},{begin:`(?x:								# extended mode
					^\\s*								# begining of line and some space
					(?i:(type))							# 1: word type
					\\s+									# some space
					([a-zA-Z_][a-zA-Z0-9_]*)			# 2: type name
					)`,beginCaptures:{1:{name:"storage.type.fortran.modern"},2:{name:"entity.name.type.fortran.modern"}},end:`(?xi)
				(end)(?=\\s|\\z)						# 1: the word end
				(?:
					\\s+
					(type)							# 2: possibly the word type
					(\\s+[A-Za-z_][A-Za-z0-9_]*)?	# 3: possibly the name
				)?
			`,endCaptures:{1:{name:"keyword.other.fortran"},2:{name:"storage.type.fortran.modern"},3:{name:"entity.name.type.end.fortran.modern"}},name:"meta.type-definition.fortran.modern",patterns:[{include:"$self"}]},{begin:"(^[ \\t]+)?(?=!-)",beginCaptures:{1:{name:"punctuation.whitespace.comment.leading.fortran"}},end:"(?!\\G)",patterns:[{begin:"!-",beginCaptures:{0:{name:"punctuation.definition.comment.fortran"}},end:"\\n",name:"comment.line.exclamation.mark.fortran.modern",patterns:[{match:"\\\\\\s*\\n"}]}]},{begin:"(^[ \\t]+)?(?=!)",beginCaptures:{1:{name:"punctuation.whitespace.comment.leading.fortran"}},end:"(?!\\G)",patterns:[{begin:"!",beginCaptures:{0:{name:"punctuation.definition.comment.fortran"}},end:"\\n",name:"comment.line.exclamation.fortran.modern",patterns:[{match:"\\\\\\s*\\n"}]}]},{match:"\\b(?i:(select\\s+case|case(\\s+default)?|end\\s+select|use|(end\\s+)?forall))\\b",name:"keyword.control.fortran.modern"},{match:"\\b(?i:(access|action|advance|append|apostrophe|asis|blank|delete|delim|direct|end|eor|err|exist|file|fmt|form|formatted|iolength|iostat|keep|name|named|nextrec|new|nml|no|null|number|old|opened|pad|position|quote|read|readwrite|rec|recl|replace|scratch|sequential|size|status|undefined|unformatted|unit|unknown|write|yes|zero|namelist)(?=\\())",name:"keyword.control.io.fortran.modern"},{match:"\\b(\\=\\=|\\/\\=|\\>\\=|\\>|\\<|\\<\\=)\\b",name:"keyword.operator.logical.fortran.modern"},{match:"(\\%|\\=\\>)",name:"keyword.operator.fortran.modern"},{match:"\\b(?i:(ceiling|floor|modulo)(?=\\())",name:"keyword.other.instrinsic.numeric.fortran.modern"},{match:"\\b(?i:(allocate|allocated|deallocate)(?=\\())",name:"keyword.other.instrinsic.array.fortran.modern"},{match:"\\b(?i:(associated)(?=\\())",name:"keyword.other.instrinsic.pointer.fortran.modern"},{match:"\\b(?i:((end\\s*)?(interface|procedure|module)))\\b",name:"keyword.other.programming-units.fortran.modern"},{begin:"\\b(?i:(type(?=\\s*\\()))\\b(?=.*::)",beginCaptures:{1:{name:"storage.type.fortran.modern"}},end:"(?=!)|$",name:"meta.specification.fortran.modern",patterns:[{include:"$base"}]},{match:"\\b(?i:(type(?=\\s*\\()))\\b",name:"storage.type.fortran.modern"},{match:"\\b(?i:(optional|recursive|pointer|allocatable|target|private|public))\\b",name:"storage.modifier.fortran.modern"}],scopeName:"source.fortran.modern"},e=t;export{e as default};
