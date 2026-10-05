import"./grammar-MHCU5XT2.js";var n={extensions:[".pkl"],names:["pkl"],patterns:[{captures:{1:{name:"variable.language.pkl"},2:{name:"variable.other.module.pkl"}},match:`(?x:
  \\b
  (module)
  \\s+
  (
    [\\p{L}_$][\\p{L}0-9_$]*(?:\\.[\\p{L}_$][\\p{L}0-9_$]*)*
  )
)`},{captures:{1:{name:"keyword.class.pkl"},2:{name:"entity.name.type.pkl"},3:{name:"punctuation.pkl"},4:{name:"entity.name.type.pkl"}},match:`(?x:
  (typealias)
  \\s+
  ([\\p{L}_$][\\p{L}0-9_$]*)
  \\s*(=)\\s*
  ((?x:
  (?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
)
  \\s*
  (\\|\\s*(?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
))*
))
)`},{captures:{1:{name:"keyword.class.pkl"}},match:"\\b(class)\\s+[\\p{L}_$][\\p{L}0-9_$]*",name:"entity.name.type.pkl"},{captures:{1:{name:"keyword.control.pkl"},2:{name:"variable.other.property.pkl"},3:{name:"variable.other.property.pkl"},4:{name:"storage.modifier.pkl"}},match:`(?x:
  \\b(for)
  \\s*\\(
  ([\\p{L}_$][\\p{L}0-9_$]*)(?:\\s*,\\s*([\\p{L}_$][\\p{L}0-9_$]*))* # bindings
  \\s+
  (in)
)`},{captures:{1:{name:"keyword.control.pkl"},2:{name:"entity.name.type.pkl"}},match:`\\b(new)\\s+((?x:
  (?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
)
  \\s*
  (\\|\\s*(?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
))*
))`},{captures:{1:{name:"keyword.pkl"},2:{name:"variable.other.property.pkl"}},match:"\\b(function)\\s+([\\p{L}_$][\\p{L}0-9_$]*)"},{captures:{1:{name:"keyword.pkl"},2:{name:"entity.name.type.pkl"}},match:`\\b(as)\\s+((?x:
  (?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
)
  \\s*
  (\\|\\s*(?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
))*
))`},{match:"\\b(true|false|null)\\b",name:"constant.character.language.pkl"},{match:"//.*",name:"comment.line.pkl"},{begin:"/\\*",end:"\\*/",name:"comment.block.pkl"},{begin:`(?x:
  (
    (?:\\b|\\s*)[\\p{L}_$][\\p{L}0-9_$]* # variable name
    |
    \`[^\`]+\` # quoted variable name
  )
  \\s*
  (:)
  \\s*
  ((?x:
  (?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
)
  \\s*
  (\\|\\s*(?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
))*
)) # type
)`,captures:{1:{name:"variable.other.property.pkl"},2:{name:"punctuation.pkl"},3:{name:"entity.name.type.pkl"}},end:"\\s*=|,|\\)|^[ \\t]*$"},{captures:{1:{name:"variable.other.property.pkl"},2:{name:"punctuation.pkl"}},match:`(?x:
  (
    \\b[\\p{L}_$][\\p{L}0-9_$]* # variable name
    |
    \`[^\`]+\` # quoted variable name
  )
  \\s*
  (=)(?!=)
)`},{captures:{1:{name:"punctuation.pkl"},2:{name:"entity.name.type.pkl"}},match:`(:)\\s*((?x:
  (?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
)
  \\s*
  (\\|\\s*(?x:
  [\\p{L}_$][\\p{L}0-9_$]* # ident
  \\s*
  (?:<[^>]*>)? # optional type parameters
  \\s*
  (?:\\([^)]*\\))? # optional constraint (this is an approximation)
  \\s*
  \\?? # optional nulability indicator
))*
))`},{captures:{1:{name:"variable.other.property.pkl"}},match:"^\\s*([\\p{L}_$][\\p{L}0-9_$]*)\\s*\\{"},{match:"\\b(hidden|local|abstract|external|open|in|out|amends|extends|fixed|const)\\b",name:"storage.modifier.pkl"},{match:"\\b(amends|as|extends|function|is|let|read|read\\?|import|throw|trace)\\b",name:"keyword.pkl"},{match:"\\b(if|else|when|for|import|new)\\b",name:"keyword.control.pkl"},{match:`(?x:
  \\b
  0x(?:[\\da-fA-F][\\da-fA-F_]*[\\da-fA-F]|[\\da-fA-F_])
  \\b
)`,name:"constant.numeric.hex.pkl"},{match:`(?x:
  \\b
  0b(?:[0-1][0-1_]*[0-1]|[0-1])
  \\b
)`,name:"constant.numeric.binary.pkl"},{match:`(?x:
  \\b
  0o(?:[0-7][0-7_]*[0-7]|[0-7])
  \\b
)`,name:"constant.numeric.octal.pkl"},{match:`(?x:
  \\b
  (?:\\d[0-9_]*\\d|\\d)
  \\b
)`,name:"constant.numeric.decimal.pkl"},{match:`(?x:
  \\b
  (?:
    (?:\\d[0-9_]*\\d|\\d)?              # 0 or more digits
    \\.                               # dot literal
    (?:\\d[0-9_]*\\d|\\d)               # 1 or more digits
    (?:[eE][+-]?(?:\\d[0-9_]*\\d|\\d))? # optional exponent
    |                                # OR
    (?:\\d[0-9_]*\\d|\\d)               # 1 or more digits
    [eE][+-]?(?:\\d[0-9_]*\\d|\\d)      # exponent
  )
  \\b
)`,name:"constant.numeric.pkl"},{match:`(?x:
  # MATH
  \\+    # add
  |
  -     # minus
  |
  \\*    # multiply
  |
  /     # divide
  |
  ~/    # integer divide
  |
  %     # modulo
  |
  \\*\\*  # power
  |
  >     # greater than
  |
  >=    # greater than or equals
  |
  <     # less than
  |
  <=    # less than or equals
  |
  ==    # equals
  |
  !=    # not equals

  # LOGICAL
  |
  !     # unary not
  |
  &&    # and
  |
  \\|\\|  # or
  |

  # MISCELLANEOUS
  \\|>   # function pipe
  |
  \\?\\?  # nullish coalesce
  |
  !!    # non-null assertion
  |
  =     # assignment
  |
  ->    # lambda arrow
  |
  \\|    # type union
)`,name:"keyword.operator.pkl"},{match:"\\b(this|module|outer|super)\\b",name:"variable.language.pkl"},{match:"\\b(unknown|never)\\b",name:"support.type.pkl"},{match:"[(){}\\[\\]]",name:"meta.brace.pkl"},{match:"\\b(class|typealias)\\b",name:"keyword.class.pkl"},{match:`(?x:
  \\.\\?  # optional chain
  |
  \\.    # member access
  |
  ;     # semicolon
  |
  :     # colon
)`,name:"punctuation.pkl"},{match:"@[\\p{L}_$][\\p{L}0-9_$]*",name:"entity.name.type.pkl"},{begin:'(""")',captures:{1:{name:"punctuation.delimiter.pkl"}},end:'(""")',name:"string.quoted.triple.0.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.0.pkl"}]},{begin:'(")',beginCaptures:{1:{name:"punctuation.delimiter.pkl"}},end:`(?x:
  (")         # string end
  |                     # OR
  (.?$)                 # error; unterminated string (flag last character as an error)
)`,endCaptures:{1:{name:"punctuation.delimimter.pkl"},2:{name:"invalid.illegal.newline.pkl"}},name:"string.quoted.double.0.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.0.pkl"}]},{begin:'(#""")',captures:{1:{name:"punctuation.delimiter.pkl"}},end:'("""#)',name:"string.quoted.triple.1.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.1.pkl"}]},{begin:'(#")',beginCaptures:{1:{name:"punctuation.delimiter.pkl"}},end:`(?x:
  ("\\#)         # string end
  |                     # OR
  (.?$)                 # error; unterminated string (flag last character as an error)
)`,endCaptures:{1:{name:"punctuation.delimimter.pkl"},2:{name:"invalid.illegal.newline.pkl"}},name:"string.quoted.double.1.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.1.pkl"}]},{begin:'(##""")',captures:{1:{name:"punctuation.delimiter.pkl"}},end:'("""##)',name:"string.quoted.triple.2.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.2.pkl"}]},{begin:'(##")',beginCaptures:{1:{name:"punctuation.delimiter.pkl"}},end:`(?x:
  ("\\#\\#)         # string end
  |                     # OR
  (.?$)                 # error; unterminated string (flag last character as an error)
)`,endCaptures:{1:{name:"punctuation.delimimter.pkl"},2:{name:"invalid.illegal.newline.pkl"}},name:"string.quoted.double.2.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.2.pkl"}]},{begin:'(###""")',captures:{1:{name:"punctuation.delimiter.pkl"}},end:'("""###)',name:"string.quoted.triple.3.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.3.pkl"}]},{begin:'(###")',beginCaptures:{1:{name:"punctuation.delimiter.pkl"}},end:`(?x:
  ("\\#\\#\\#)         # string end
  |                     # OR
  (.?$)                 # error; unterminated string (flag last character as an error)
)`,endCaptures:{1:{name:"punctuation.delimimter.pkl"},2:{name:"invalid.illegal.newline.pkl"}},name:"string.quoted.double.3.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.3.pkl"}]},{begin:'(####""")',captures:{1:{name:"punctuation.delimiter.pkl"}},end:'("""####)',name:"string.quoted.triple.4.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.4.pkl"}]},{begin:'(####")',beginCaptures:{1:{name:"punctuation.delimiter.pkl"}},end:`(?x:
  ("\\#\\#\\#\\#)         # string end
  |                     # OR
  (.?$)                 # error; unterminated string (flag last character as an error)
)`,endCaptures:{1:{name:"punctuation.delimimter.pkl"},2:{name:"invalid.illegal.newline.pkl"}},name:"string.quoted.double.4.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.4.pkl"}]},{begin:'(#####""")',captures:{1:{name:"punctuation.delimiter.pkl"}},end:'("""#####)',name:"string.quoted.triple.5.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#\\#\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#\\#\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.5.pkl"}]},{begin:'(#####")',beginCaptures:{1:{name:"punctuation.delimiter.pkl"}},end:`(?x:
  ("\\#\\#\\#\\#\\#)         # string end
  |                     # OR
  (.?$)                 # error; unterminated string (flag last character as an error)
)`,endCaptures:{1:{name:"punctuation.delimimter.pkl"},2:{name:"invalid.illegal.newline.pkl"}},name:"string.quoted.double.5.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#\\#\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#\\#\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.5.pkl"}]},{begin:'(######""")',captures:{1:{name:"punctuation.delimiter.pkl"}},end:'("""######)',name:"string.quoted.triple.6.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#\\#\\#\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#\\#\\#\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.6.pkl"}]},{begin:'(######")',beginCaptures:{1:{name:"punctuation.delimiter.pkl"}},end:`(?x:
  ("\\#\\#\\#\\#\\#\\#)         # string end
  |                     # OR
  (.?$)                 # error; unterminated string (flag last character as an error)
)`,endCaptures:{1:{name:"punctuation.delimimter.pkl"},2:{name:"invalid.illegal.newline.pkl"}},name:"string.quoted.double.6.pkl",patterns:[{captures:{1:{name:"invalid.illegal.unrecognized-string-escape.pkl"}},match:`(?x:                 # turn on extended mode
  \\\\\\#\\#\\#\\#\\#\\#
  (?:
    [trn"\\\\]         # tab, carriage return, newline, quote, backslash
    |                # OR
    u                # the letter u
    \\{               # curly opening brace literal
    [\\da-fA-F]+      # 1 or more hex number literal
    }                # curly end literal
    |                # OR
    \\(               # interpolation start
    .+?              # one or more characters lazily (correct syntax highlighting within here should be provided by semantic tokens)
    \\)               # interpolation end
  )
  |                  # OR
  (                  # capture group: invalid escape
    \\\\\\#\\#\\#\\#\\#\\#   # the escape char
    .                # any character
  )
)`,name:"constant.character.escape.6.pkl"}]}],scopeName:"source.pkl"},e=n;export{e as default};
