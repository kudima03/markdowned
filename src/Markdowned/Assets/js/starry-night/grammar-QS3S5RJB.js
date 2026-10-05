import"./grammar-MHCU5XT2.js";var e={extensions:[".just"],names:["just","justfile"],patterns:[{include:"#comments"},{include:"#import"},{include:"#module"},{include:"#alias"},{include:"#assignment"},{include:"#builtins"},{include:"#keywords"},{include:"#expression-operators"},{include:"#backtick"},{include:"#strings"},{include:"#parenthesis"},{include:"#recipes"},{include:"#recipe-operators"},{include:"#embedded-languages"},{include:"#escaping"}],repository:{alias:{captures:{1:{name:"keyword.other.reserved.just"},2:{name:"variable.name.alias.just"},3:{name:"keyword.operator.assignment.just"},4:{name:"variable.other.just"}},match:`(?x)
  ^
  (alias) \\s+ 
  ([a-zA-Z_][a-zA-Z0-9_-]*) \\s* 
  (:=) \\s* 
  ([a-zA-Z_][a-zA-Z0-9_-]*)
`},assignment:{patterns:[{include:"#variable-assignment"},{include:"#setting-assignment"}]},backtick:{patterns:[{begin:"(```)",beginCaptures:{1:{name:"string.interpolated.just"}},contentName:"source.shell",end:"(```)",endCaptures:{1:{name:"string.interpolated.just"}},patterns:[{include:"source.shell"}]},{captures:{1:{name:"string.interpolated.just"},2:{name:"source.shell",patterns:[{include:"source.shell"}]},3:{name:"string.interpolated.just"}},match:"(`)([^`]*)(`)"}]},boolean:{patterns:[{match:"\\b(true|false)\\b",name:"constant.language.boolean.just"}]},"builtin-functions":{patterns:[{match:`(?x) \\b(
  arch|num_cpus|os|os_family|shell|env_var|env_var_or_default|env|
  is_dependency|invocation_directory|invocation_dir|invocation_directory_native|
  invocation_dir_native|justfile|justfile_directory|justfile_dir|just_executable|
  just_pid|source_file|source_directory|source_dir|module_file|module_directory|
  module_dir|append|prepend|encode_uri_component|quote|replace|replace_regex|
  trim|trim_end|trim_end_match|trim_end_matches|trim_start|trim_start_match|
  trim_start_matches|capitalize|kebabcase|lowercamelcase|lowercase|
  shoutykebabcase|shoutysnakecase|snakecase|titlecase|uppercamelcase|
  uppercase|absolute_path|blake3|blake3_file|canonicalize|extension|
  file_name|file_stem|parent_directory|parent_dir|without_extension|clean|join|
  path_exists|error|assert|sha256|sha256_file|uuid|choose|datetime|
  datetime_utc|semver_matches|style|cache_directory|cache_dir|config_directory|config_dir|
  config_local_directory|config_local_dir|data_directory|data_dir|data_local_directory|
  data_local_dir|executable_directory|executable_dir|home_directory|home_dir|which|require|read
)\\b
`,name:"support.function.builtin.just"}]},builtins:{patterns:[{match:`(?x) \\b(
  HEX|HEXLOWER|HEXUPPER|PATH_SEP|PATH_VAR_SEP|CLEAR|NORMAL|BOLD|ITALIC|UNDERLINE|INVERT|HIDE|
  STRIKETHROUGH|BLACK|RED|GREEN|YELLOW|BLUE|MAGENTA|CYAN|WHITE|BG_BLACK|
  BG_RED|BG_GREEN|BG_YELLOW|BG_BLUE|BG_MAGENTA|BG_CYAN|BG_WHITE
)\\b
`,name:"constant.language.const.just"},{include:"#builtin-functions"},{include:"#literal"}]},comments:{patterns:[{match:"#(?!\\!).*$",name:"comment.line.number-sign.just"}]},"control-keywords":{patterns:[{match:"\\b(if|else)\\b",name:"keyword.control.conditional.just"}]},"embedded-languages":{patterns:[{begin:"^\\s+(#!/usr/bin/env\\s+(?:-S\\s+)?node.*)$",beginCaptures:{1:{name:"comment.line.number-sign.shebang.just"}},contentName:"source.js",patterns:[{include:"source.js"}],while:"^(?=\\s*$|\\s)"},{begin:"^\\s+(#!/usr/bin/env\\s+(?:-S\\s+)?deno.*)$",beginCaptures:{1:{name:"comment.line.number-sign.shebang.just"}},contentName:"source.ts",patterns:[{include:"source.ts"}],while:"^(?=\\s*$|\\s)"},{begin:"^\\s+(#!/usr/bin/env\\s+(?:-S\\s+)?perl.*)$",beginCaptures:{1:{name:"comment.line.number-sign.shebang.just"}},contentName:"source.perl",patterns:[{include:"source.perl"}],while:"^(?=\\s*$|\\s)"},{begin:"^\\s+(#!/usr/bin/env\\s+(?:-S\\s+)?python.*)$",beginCaptures:{1:{name:"comment.line.number-sign.shebang.just"}},contentName:"source.python",patterns:[{include:"source.python"}],while:"^(?=\\s*$|\\s)"},{begin:"^\\s+(#!/usr/bin/env\\s+(?:-S\\s+)?ruby.*)$",beginCaptures:{1:{name:"comment.line.number-sign.shebang.just"}},contentName:"source.ruby",patterns:[{include:"source.ruby"}],while:"^(?=\\s*$|\\s)"},{begin:"^\\s+(#!/usr/bin/env\\s+(?:-S\\s+)?(?:sh|bash|zsh|fish).*)$",beginCaptures:{1:{name:"comment.line.number-sign.shebang.just"}},contentName:"source.shell",patterns:[{include:"source.shell"}],while:"^(?=\\s*$|\\s)"}]},escaping:{patterns:[{captures:{1:{name:"string.interpolated.escape.just"},2:{patterns:[{include:"#expression"}]},3:{name:"string.interpolated.escape.just"}},match:`(?x)
  (?<!\\{)
  (\\{\\{)
    \\{? (?!\\{)
    (.*?)
  (\\}\\})
`,name:"string.interpolated.escaping.just"}]},expression:{patterns:[{include:"#backtick"},{include:"#builtins"},{include:"#control-keywords"},{include:"#expression-operators"},{include:"#parenthesis"},{include:"#strings"}]},"expression-operators":{patterns:[{match:"\\/",name:"keyword.operator.path-join.just"},{match:"\\+",name:"keyword.operator.concat.just"},{match:"&&",name:"keyword.operator.and.just"},{match:"\\|\\|",name:"keyword.operator.or.just"},{match:"(\\=\\=|\\=\\~|\\!\\=)",name:"keyword.operator.equality.just"}]},import:{begin:`(?x)
  ^
  (import)
  (\\?)? \\s+
`,beginCaptures:{1:{name:"keyword.other.reserved.just"},2:{name:"punctuation.optional.just"}},end:"$",patterns:[{include:"#strings"}]},keywords:{patterns:[{include:"#reserved-keywords"},{include:"#control-keywords"}]},literal:{patterns:[{include:"#boolean"},{include:"#number"}]},module:{begin:`(?x)
  ^
  (mod)
  (\\?)? \\s+
  ([a-zA-Z_][a-zA-Z0-9_-]*)
  (?=[$\\s])
`,beginCaptures:{1:{name:"keyword.other.reserved.just"},2:{name:"punctuation.optional.just"},3:{name:"variable.name.module.just"}},end:"$",patterns:[{include:"#strings"}]},number:{patterns:[{match:`(?x)
  (?<! [a-zA-Z_\\-])(?:
    \\. \\d+
    |
    \\d+ \\. \\d+
    |
    \\d+ \\.
    |
    [1-9] \\d*
  )
`,name:"constant.numeric.just"},{match:"\\b[0-9]+[a-zA-Z_\\-]+\\b",name:"invalid.illegal.name.just"}]},parenthesis:{begin:"\\(",end:"\\)",patterns:[{include:"#expression"},{include:"#parenthesis"}]},"recipe-attributes":{patterns:[{captures:{1:{name:"support.function.system.just"},2:{name:"support.function.system.just"}},match:`(?x)
  ^
  \\[ 
    ([a-zA-z\\-]+) \\s*
    (?: , ( \\s* [a-zA-z\\-]+ \\s* ) )*
  ] \\s*
  $
`},{captures:{1:{name:"support.function.system.just"},2:{name:"keyword.operator.attribute.end.just"},3:{patterns:[{include:"#strings"}]},4:{patterns:[{include:"#strings"}]}},match:`(?x)
  ^
  \\[
    ([a-zA-z\\-]+)
    (?: 
      (?: (:) (.*?) ) | (\\( (.*?) \\))
    )?
  ] \\s*
  $
`}]},"recipe-dependencies":{captures:{1:{name:"entity.name.function.just"},2:{patterns:[{captures:{1:{name:"entity.name.function.just"},2:{patterns:[{include:"#expression"}]}},match:`(?x)
  \\( 
    (?: 
      ([a-zA-Z_][a-zA-Z0-9_\\-]*)
      (.*)
    )
  \\)
`}]},3:{name:"keyword.operator.and.just"}},match:`(?x)
  (?:
    ([a-zA-Z_][a-zA-Z0-9_\\-]*)
    | ( \\( 
        (?: [^\\(\\)]+ | \\( [^\\)]* \\))* 
      \\) )
    | (&&)
  )
`},"recipe-operators":{patterns:[{captures:{1:{name:"keyword.operator.quiet.just"}},match:"^\\s+(@)"},{captures:{1:{name:"keyword.operator.error-suppression.just"}},match:"^\\s+(\\-)"}]},"recipe-params":{captures:{1:{name:"keyword.other.recipe.variadic.just"},2:{name:"variable.parameter.recipe.just"},3:{name:"keyword.operator.default.just"},4:{patterns:[{include:"#strings"}]},5:{patterns:[{include:"#backtick"}]},6:{patterns:[{include:"#parenthesis"}]}},match:`(?x)
  (?: 
    (\\+|\\*|\\$)?
    ([a-zA-Z_][a-zA-Z_0-9]*
  )
  (?:
    (=)
    (?: 
      [a-zA-Z_][a-zA-Z_0-9]* 
      | (\\".*?\\" | '.*?') 
      | (\`.*?\`) 
      | ( \\( 
          (?: 
            [^\\(\\)]+ 
            | \\( [^)]* \\)
          )* \\) ) 
        ) 
    )?
  )
`},recipes:{patterns:[{captures:{1:{name:"keyword.other.recipe.prefix.just"},2:{name:"entity.name.function.just"},3:{patterns:[{include:"#recipe-params"}]},4:{name:"keyword.operator.recipe.end.just"},5:{patterns:[{include:"#recipe-dependencies"}]}},match:`(?x)
  ^
  (@_|_@|@|_)?
  ([a-zA-Z][a-zA-Z0-9_\\-]*)
  (?: \\s+ (.*?) )?
  \\s* (:)
  (.*)
`},{include:"#recipe-operators"},{include:"#recipe-attributes"},{include:"#embedded-languages"}]},"reserved-keywords":{patterns:[{captures:{1:{name:"keyword.other.reserved.just"}},match:"^(alias|export|unexport|import|mod|set)\\s+"}]},"setting-assignment":{patterns:[{begin:`(?x) 
  ^
  (set) \\s+
  ([a-zA-Z_][a-zA-Z0-9_-]*) \\s*
  (:=)?
`,beginCaptures:{1:{name:"keyword.other.reserved.just"},2:{name:"variable.other.just"},3:{name:"keyword.operator.assignment.just"}},end:"$",patterns:[{include:"#expression"},{include:"#comments"}]}]},strings:{patterns:[{match:`([\\"']{1,3})[\\{]+(\\1)`,name:"string.quoted.double.indented.just"},{begin:'(f|x)?(""")',beginCaptures:{1:{name:"constant.character.expanded.just"},2:{name:"string.quoted.double.indented.just"}},end:'"""',name:"string.quoted.double.indented.just",patterns:[{match:"\\\\.(?:(?<=u)\\{.+?\\})?",name:"constant.character.escape.just"},{include:"#escaping"}]},{begin:'(f|x)?(")',beginCaptures:{1:{name:"constant.character.expanded.just"},2:{name:"string.quoted.double.just"}},end:'"',name:"string.quoted.double.just",patterns:[{match:"\\\\.(?:(?<=u)\\{.+?\\})?",name:"constant.character.escape.just"},{include:"#escaping"}]},{begin:"(f|x)?(''')",beginCaptures:{1:{name:"constant.character.expanded.just"},2:{name:"string.quoted.single.indented.just"}},end:"'''",name:"string.quoted.single.indented.just",patterns:[{include:"#escaping"}]},{begin:"(f|x)?(')",beginCaptures:{1:{name:"constant.character.expanded.just"},2:{name:"string.quoted.single.just"}},end:"'",name:"string.quoted.single.just",patterns:[{include:"#escaping"}]}]},"variable-assignment":{patterns:[{captures:{1:{name:"keyword.other.reserved.just"},2:{name:"variable.other.just"}},match:"^(unexport)\\s+([a-zA-Z_][a-zA-Z0-9_-]*)"},{begin:`(?x) 
  ^
  (?: (export) \\s+)?
  ([a-zA-Z_][a-zA-Z0-9_-]*) \\s*
  (:=)
`,beginCaptures:{1:{name:"keyword.other.reserved.just"},2:{name:"variable.other.just"},3:{name:"keyword.operator.assignment.just"}},end:"$",patterns:[{include:"#expression"},{include:"#comments"}]}]}},scopeName:"source.just"},n=e;export{n as default};
