import"./grammar-MHCU5XT2.js";var e={extensions:[".edgeql",".esdl"],names:["edgeql","esdl"],patterns:[{include:"#all"}],repository:{all:{patterns:[{include:"#fnstatement"},{include:"#expressions"},{match:"(;)",name:"punctuation.statement.delimiter.edgeql"}]},"array-dimensions":{begin:"(\\[)",beginCaptures:{1:{name:"punctuation.parenthesis.begin.edgeql"}},end:"(\\])",endCaptures:{1:{name:"punctuation.parenthesis.end.edgeql"}},patterns:[{match:"(\\d+)",name:"constant.numeric.edgeql"},{match:"\\S+",name:"invalid.illegal.type.edgeql"}]},"builtin-indexes":{match:`(?x) \\b(?<!\\.|\\.<|\\.>) (
  brin | btree | gin | gist |
  hash | index | spgist
)\\b
`,name:"support.other.index.builtin.edgeql"},"builtin-modules":{match:`(?x) \\b(?<!::|\\.|\\.<|\\.>)(
  cal | cfg | enc | ext |
  fts | go | http | js |
  lang | math | net | perm |
  pg | py | rs | schema |
  std | sys
)\\b
`,name:"support.other.module.builtin.edgeql"},"builtin-types":{match:`(?x) \\b(?<!\\.|\\.<|\\.>) (
  Base64Alphabet | BaseObject | ElasticLanguage | Endian |
  FreeObject | JsonEmpty | Language | LuceneLanguage |
  Method | Object | PGLanguage | RequestFailureKind |
  RequestState | Response | ScheduledRequest | Weight |
  anycontiguous | anydiscrete | anyenum | anyfloat |
  anyint | anynumeric | anypoint | anyreal |
  anyscalar | anytype | array | bigint |
  bool | bytes | date | date_duration |
  datetime | decimal | document | duration |
  enum | float32 | float64 | int16 |
  int32 | int64 | interval | json |
  local_date | local_datetime | local_time | multirange |
  range | relative_duration | sequence | str |
  timestamp | timestamptz | tuple | uuid
)\\b
`,name:"support.type.builtin.edgeql"},builtins:{patterns:[{match:`(?x) \\b(?<!\\.|\\.<|\\.>) (
  abs | acos | adjacent |
  all | any | approximate_count |
  array_agg | array_fill | array_get |
  array_insert | array_join | array_replace |
  array_set | array_unpack | asin |
  assert | assert_distinct | assert_exists |
  assert_single | atan | atan2 |
  bit_and | bit_count | bit_lshift |
  bit_not | bit_or | bit_rshift |
  bit_xor | bounded_above | bounded_below |
  bytes_get_bit | ceil | contains |
  cos | cot | count |
  date_get | datetime_current | datetime_get |
  datetime_of_statement | datetime_of_transaction | datetime_truncate |
  duration_get | duration_normalize_days | duration_normalize_hours |
  duration_to_seconds | duration_truncate | enumerate |
  find | floor | get_current_branch |
  get_current_database | get_instance_name | get_transaction_isolation |
  get_version | get_version_as_str | json_array_unpack |
  json_get | json_object_pack | json_object_unpack |
  json_set | json_typeof | len |
  lg | ln | log |
  materialized | max | mean |
  min | multirange | multirange_unpack |
  overlaps | pi | random |
  range | range_get_lower | range_get_upper |
  range_is_empty | range_is_inclusive_lower | range_is_inclusive_upper |
  range_unpack | re_match | re_match_all |
  re_replace | re_test | reset_query_stats |
  round | search | sequence_next |
  sequence_reset | sin | sqrt |
  stddev | stddev_pop | str_lower |
  str_lpad | str_ltrim | str_pad_end |
  str_pad_start | str_repeat | str_replace |
  str_reverse | str_rpad | str_rtrim |
  str_split | str_title | str_trim |
  str_trim_end | str_trim_start | str_upper |
  strictly_above | strictly_below | sum |
  tan | time_get | to_bigint |
  to_bytes | to_date_duration | to_datetime |
  to_decimal | to_duration | to_float32 |
  to_float64 | to_int16 | to_int32 |
  to_int64 | to_json | to_local_date |
  to_local_datetime | to_local_time | to_relative_duration |
  to_str | to_uuid | uuid_generate_v1mc |
  uuid_generate_v4 | var | var_pop |
  with_options
)(?=\\s*\\()\\b
`,name:"support.function.builtin.edgeql"},{match:`(?x) \\b(?<!\\.|\\.<|\\.>) (
  constraint | exclusive | expression |
  len_value | max_ex_value | max_len_value |
  max_value | min_ex_value | min_len_value |
  min_value | one_of | regexp
)\\b
`,name:"support.function.constraint.builtin.edgeql"},{include:"#builtin-modules"},{match:`(?x) \\b(
  __default__ | __edgedbsys__ | __edgedbtpl__ |
  __new__ | __old__ | __source__ |
  __specified__ | __std__ | __subject__ |
  __type__
)\\b
`,name:"support.other.link.builtin.edgeql"}]},bytes:{patterns:[{begin:"(b)(')",beginCaptures:{1:{name:"storage.type.string.edgeql"},2:{name:"punctuation.definition.string.begin.edgeql"}},end:"(\\2)",endCaptures:{1:{name:"punctuation.definition.string.end.edgeql"}},name:"string.quoted.bytes.edgeql",patterns:[{include:"#bytes-escapes"},{match:"([\\n -&(-\\[\\]-~])+"},{match:"(\\\\x.{1,2})|(\\\\.)|(.)",name:"invalid.illegal.bytes.edgeql"}]},{begin:'(b)(")',beginCaptures:{1:{name:"storage.type.string.edgeql"},2:{name:"punctuation.definition.string.begin.edgeql"}},end:"(\\2)",endCaptures:{1:{name:"punctuation.definition.string.end.edgeql"}},name:"string.quoted.bytes.edgeql",patterns:[{include:"#bytes-escapes"},{match:"([\\n -!#-\\[\\]-~])+"},{match:"(\\\\x.{1,2})|(\\\\.)|(.)",name:"invalid.illegal.bytes.edgeql"}]}]},"bytes-escapes":{match:`(?x)
  (
    \\\\[\\\\'"bfnrt] |
    \\\\x[0-9a-fA-F]{2}
  )
`,name:"constant.character.escape.edgeql"},casts:{begin:`(?xi)
  (?:
    (?<= ^ | [@~+\\-*/%^<>=?,:(\\[{])
    |
    (?<=
      AND | OR | NOT | LIKE | ILIKE | IS | IN | IF | ELSE |
      UNION | ALL | EXISTS |

      SELECT | GROUP | UPDATE | BY | THEN | LIMIT |
      # there are some ligature-related issues with "fi" and "ff"
      F[Ii]LTER | OF[Ff]SET
    )
  ) \\s* (\\<)
`,beginCaptures:{1:{name:"keyword.operator.cast.begin.edgeql"}},contentName:"meta.typecast.edgeql",end:"(\\>)",endCaptures:{1:{name:"keyword.operator.cast.end.edgeql"}},patterns:[{include:"#types"},{include:"#types-common"}]},code:{patterns:[{captures:{1:{name:"keyword.declaration.edgeql"},3:{name:"keyword.declaration.edgeql"}},match:`(?xi)
  \\b(FROM) \\s+ (EDGEQL | SQL) \\s+ (EXPRESSION)
`},{begin:`(?xi)
  \\b(FROM) \\s+
  (SQL) \\s+
  (\\$\\w?\\$)
`,beginCaptures:{1:{name:"keyword.declaration.edgeql"},3:{name:"string.quoted.edgeql"}},end:"(\\3)",endCaptures:{1:{name:"string.quoted.edgeql"}},patterns:[{include:"source.sql"}]},{begin:`(?xi)
  \\b(FROM) \\s+
  (EDGEQL) \\s+
  (\\$\\w?\\$)
`,beginCaptures:{1:{name:"keyword.declaration.edgeql"},3:{name:"string.quoted.edgeql"}},end:"(\\3)",endCaptures:{1:{name:"string.quoted.edgeql"}},patterns:[{include:"source.edgeql"}]}]},commandblock:{begin:"(?=SET|CREATE|ALTER|DROP|RENAME|FROM)",end:"(?=\\})",patterns:[{include:"#all"}]},comments:{patterns:[{captures:{1:{name:"punctuation.definition.comment.edgeql"},2:{name:"comment.line.note.notation.edgeql"},3:{name:"comment.line.note.edgeql"}},match:"(#)\\s*((BUG|FIXME|TODO|XXX)).*$\\n?",name:"comment.line.number-sign.edgeql"},{captures:{1:{name:"punctuation.definition.comment.edgeql"}},match:"(#).*$\\n?",name:"comment.line.number-sign.edgeql"}]},curlybraces:{begin:"(\\{)",beginCaptures:{1:{name:"punctuation.parenthesis.begin.edgeql"}},end:"(\\})",endCaptures:{1:{name:"punctuation.parenthesis.end.edgeql"}},patterns:[{include:"#comments"},{include:"#commandblock"},{include:"#shape"}]},definition:{captures:{1:{name:"variable.parameter.definition.edgeql"},2:{name:"invalid.illegal.definition.edgeql"}},match:`(?x)
  (?:
    ([[:alpha:]_][[:alnum:]_]*)
    |
    ([\\.\\d]\\S*?)
  ) (?=\\s*:=)
`},expressions:{patterns:[{include:"#comments"},{include:"#code"},{include:"#keywords"},{include:"#fncalls"},{include:"#operators"},{include:"#builtins"},{include:"#types"},{include:"#quoted-name"},{include:"#values"},{include:"#link-properties"},{include:"#variables"},{include:"#parentheses"},{include:"#squarebraces"},{include:"#curlybraces"},{include:"#casts"}]},fncallargs:{patterns:[{include:"#definition"},{include:"#expressions"},{match:"(,)",name:"punctuation.separator.arguments.edgeql"},{match:"(;)",name:"invalid.illegal.delimiter.edgeql"}]},fncalls:{patterns:[{begin:`(?x)
  \\b(?<!\\.|\\.<|\\.>)
  # function name
  (?:
    (
      # functions
      abs | acos | adjacent |
      all | any | approximate_count |
      array_agg | array_fill | array_get |
      array_insert | array_join | array_replace |
      array_set | array_unpack | asin |
      assert | assert_distinct | assert_exists |
      assert_single | atan | atan2 |
      bit_and | bit_count | bit_lshift |
      bit_not | bit_or | bit_rshift |
      bit_xor | bounded_above | bounded_below |
      bytes_get_bit | ceil | contains |
      cos | cot | count |
      date_get | datetime_current | datetime_get |
      datetime_of_statement | datetime_of_transaction | datetime_truncate |
      duration_get | duration_normalize_days | duration_normalize_hours |
      duration_to_seconds | duration_truncate | enumerate |
      find | floor | get_current_branch |
      get_current_database | get_instance_name | get_transaction_isolation |
      get_version | get_version_as_str | json_array_unpack |
      json_get | json_object_pack | json_object_unpack |
      json_set | json_typeof | len |
      lg | ln | log |
      materialized | max | mean |
      min | multirange | multirange_unpack |
      overlaps | pi | random |
      range | range_get_lower | range_get_upper |
      range_is_empty | range_is_inclusive_lower | range_is_inclusive_upper |
      range_unpack | re_match | re_match_all |
      re_replace | re_test | reset_query_stats |
      round | search | sequence_next |
      sequence_reset | sin | sqrt |
      stddev | stddev_pop | str_lower |
      str_lpad | str_ltrim | str_pad_end |
      str_pad_start | str_repeat | str_replace |
      str_reverse | str_rpad | str_rtrim |
      str_split | str_title | str_trim |
      str_trim_end | str_trim_start | str_upper |
      strictly_above | strictly_below | sum |
      tan | time_get | to_bigint |
      to_bytes | to_date_duration | to_datetime |
      to_decimal | to_duration | to_float32 |
      to_float64 | to_int16 | to_int32 |
      to_int64 | to_json | to_local_date |
      to_local_datetime | to_local_time | to_relative_duration |
      to_str | to_uuid | uuid_generate_v1mc |
      uuid_generate_v4 | var | var_pop |
      with_options
    |
      # constraints
      constraint | exclusive | expression |
      len_value | max_ex_value | max_len_value |
      max_value | min_ex_value | min_len_value |
      min_value | one_of | regexp
    )
    |
    ([[:alpha:]_][[:alnum:]_]*)
    |
    (\`.*?\`)
  ) \\s*(\\()
`,beginCaptures:{1:{name:"support.function.builtin.edgeql"},2:{name:"entity.name.function.edgeql"},3:{name:"string.interpolated.edgeql"},4:{name:"punctuation.definition.arguments.begin.edgeql"}},end:"(\\))",endCaptures:{1:{name:"punctuation.definition.arguments.end.edgeql"}},name:"meta.function-call.edgeql",patterns:[{include:"#fncallargs"}]},{begin:`(?x)
  \\b(?<!\\.|\\.<|\\.>)
  # module
  (?:
    (
      cal | cfg | enc |
      ext | fts | go |
      http | js | lang |
      math | net | perm |
      pg | py | rs |
      schema | std | sys
    )
    |
    (?# masking built-ins in odd ways)
    (
      #functions
      abs | acos | adjacent |
      all | any | approximate_count |
      array_agg | array_fill | array_get |
      array_insert | array_join | array_replace |
      array_set | array_unpack | asin |
      assert | assert_distinct | assert_exists |
      assert_single | atan | atan2 |
      bit_and | bit_count | bit_lshift |
      bit_not | bit_or | bit_rshift |
      bit_xor | bounded_above | bounded_below |
      bytes_get_bit | ceil | contains |
      cos | cot | count |
      date_get | datetime_current | datetime_get |
      datetime_of_statement | datetime_of_transaction | datetime_truncate |
      duration_get | duration_normalize_days | duration_normalize_hours |
      duration_to_seconds | duration_truncate | enumerate |
      find | floor | get_current_branch |
      get_current_database | get_instance_name | get_transaction_isolation |
      get_version | get_version_as_str | json_array_unpack |
      json_get | json_object_pack | json_object_unpack |
      json_set | json_typeof | len |
      lg | ln | log |
      materialized | max | mean |
      min | multirange | multirange_unpack |
      overlaps | pi | random |
      range | range_get_lower | range_get_upper |
      range_is_empty | range_is_inclusive_lower | range_is_inclusive_upper |
      range_unpack | re_match | re_match_all |
      re_replace | re_test | reset_query_stats |
      round | search | sequence_next |
      sequence_reset | sin | sqrt |
      stddev | stddev_pop | str_lower |
      str_lpad | str_ltrim | str_pad_end |
      str_pad_start | str_repeat | str_replace |
      str_reverse | str_rpad | str_rtrim |
      str_split | str_title | str_trim |
      str_trim_end | str_trim_start | str_upper |
      strictly_above | strictly_below | sum |
      tan | time_get | to_bigint |
      to_bytes | to_date_duration | to_datetime |
      to_decimal | to_duration | to_float32 |
      to_float64 | to_int16 | to_int32 |
      to_int64 | to_json | to_local_date |
      to_local_datetime | to_local_time | to_relative_duration |
      to_str | to_uuid | uuid_generate_v1mc |
      uuid_generate_v4 | var | var_pop |
      with_options
      |
      #constraints
      constraint | exclusive | expression |
      len_value | max_ex_value | max_len_value |
      max_value | min_ex_value | min_len_value |
      min_value | one_of | regexp
    )
    |
    ([[:alpha:]_][[:alnum:]_]*)
    |
    (\`.*?\`)
  )

  \\s*(::)\\s*

  # function name
  (?:
    (
      #functions
      abs | acos | adjacent |
      all | any | approximate_count |
      array_agg | array_fill | array_get |
      array_insert | array_join | array_replace |
      array_set | array_unpack | asin |
      assert | assert_distinct | assert_exists |
      assert_single | atan | atan2 |
      bit_and | bit_count | bit_lshift |
      bit_not | bit_or | bit_rshift |
      bit_xor | bounded_above | bounded_below |
      bytes_get_bit | ceil | contains |
      cos | cot | count |
      date_get | datetime_current | datetime_get |
      datetime_of_statement | datetime_of_transaction | datetime_truncate |
      duration_get | duration_normalize_days | duration_normalize_hours |
      duration_to_seconds | duration_truncate | enumerate |
      find | floor | get_current_branch |
      get_current_database | get_instance_name | get_transaction_isolation |
      get_version | get_version_as_str | json_array_unpack |
      json_get | json_object_pack | json_object_unpack |
      json_set | json_typeof | len |
      lg | ln | log |
      materialized | max | mean |
      min | multirange | multirange_unpack |
      overlaps | pi | random |
      range | range_get_lower | range_get_upper |
      range_is_empty | range_is_inclusive_lower | range_is_inclusive_upper |
      range_unpack | re_match | re_match_all |
      re_replace | re_test | reset_query_stats |
      round | search | sequence_next |
      sequence_reset | sin | sqrt |
      stddev | stddev_pop | str_lower |
      str_lpad | str_ltrim | str_pad_end |
      str_pad_start | str_repeat | str_replace |
      str_reverse | str_rpad | str_rtrim |
      str_split | str_title | str_trim |
      str_trim_end | str_trim_start | str_upper |
      strictly_above | strictly_below | sum |
      tan | time_get | to_bigint |
      to_bytes | to_date_duration | to_datetime |
      to_decimal | to_duration | to_float32 |
      to_float64 | to_int16 | to_int32 |
      to_int64 | to_json | to_local_date |
      to_local_datetime | to_local_time | to_relative_duration |
      to_str | to_uuid | uuid_generate_v1mc |
      uuid_generate_v4 | var | var_pop |
      with_options
      |
      #constraints
      constraint | exclusive | expression |
      len_value | max_ex_value | max_len_value |
      max_value | min_ex_value | min_len_value |
      min_value | one_of | regexp
    )
    |
    ([[:alpha:]_][[:alnum:]_]*)
    |
    (\`.*?\`)
  ) \\s*(\\()
`,beginCaptures:{1:{name:"support.other.module.builtin.edgeql"},2:{name:"support.function.builtin.edgeql"},3:{name:"entity.name.function.edgeql"},4:{name:"string.interpolated.edgeql"},5:{name:"keyword.operator.namespace.edgeql"},6:{name:"support.function.builtin.edgeql"},7:{name:"entity.name.function.edgeql"},8:{name:"string.interpolated.edgeql"},9:{name:"punctuation.definition.arguments.begin.edgeql"}},end:"(\\))",endCaptures:{1:{name:"punctuation.definition.arguments.end.edgeql"}},name:"meta.function-call.edgeql",patterns:[{include:"#fncallargs"}]}]},fnstatement:{begin:"(?ix) \\b(?<![:\\.])(FUNCTION|AGGREGATE|ABSTRACT CONSTRAINT)\\b",beginCaptures:{1:{name:"keyword.declaration.edgeql"}},end:"(?=[^\\s\\w:]|\\bEXTENDING\\b|$)",patterns:[{include:"#builtins"},{include:"#identifier"},{match:"(::)",name:"keyword.operator.namespace.edgeql"}]},identifier:{match:"([[:alpha:]_][[:alnum:]_]*)"},keywords:{patterns:[{match:"(?i)\\b(TRUE)\\b",name:"constant.language.boolean.true.edgeql"},{match:"(?i)\\b(FALSE)\\b",name:"constant.language.boolean.false.edgeql"},{match:"(?i)\\b(EMPTY)\\b",name:"constant.language.empty.edgeql"},{match:"(?i)\\b(?<!::|\\.|\\.<|\\.>)(CONSTRAINT)\\b(?!=\\s+\\()",name:"keyword.declaration.edgeql"},{match:`(?ix) \\b(?<!::|\\.|\\.<|\\.>)(
  (?# special case)
  (named \\s+ only)
  |
  (as \\s+ text)
  |
  (all (?!\\s*\\())
  |

  (?# unreserved)
  abort | abstract | access | after | alias |
  allow | annotation | applied | as | asc |
  assignment | before | blobal | branch | cardinality |
  cast | committed | config | conflict | cube |
  current | data | database | ddl | declare |
  default | deferrable | deferred | delegated | deny |
  desc | each | empty | extension | final |
  first | force | from | function | future |
  implicit | index | infix | inheritable | instance |
  into | isolation | last | link | migration |
  multi | object | of | only | onto |
  operator | optionality | order | orphan | overloaded |
  owned | package | permission | policy | populate |
  postfix | prefix | property | proposed | pseudo |
  read | reject | release | rename | repeatable |
  required | reset | restrict | rewrite | role |
  roles | rollup | savepoint | scalar | schema |
  sdl | serializable | session | source | superuser |
  system | target | template | ternary | then |
  to | transaction | trigger | type | unless |
  using | verbose | version | view | write
  |
  (?# reserved)
  administer | alter | analyze | and | anyarray |
  anyobject | anytuple | begin | by | case |
  check | commit | configure | create | deallocate |
  delete | describe | detached | discard | distinct |
  do | drop | else | end | except |
  exists | explain | extending | fetch | filter |
  for | get | global | grant | group |
  if | ilike | import | in | insert |
  intersect | introspect | is | like | limit |
  listen | load | lock | match | module |
  move | never | not | notify | offset |
  on | optional | or | over | partition |
  prepare | raise | refresh | revoke | rollback |
  select | set | single | start | typeof |
  union | update | variadic | when | window |
  with
)\\b
`,name:"keyword.declaration.edgeql"}]},"link-properties":{begin:"(\\@)",end:"(?<=[[:alnum:]_`])",name:"support.other.linkproperty.edgeql",patterns:[{include:"#identifier"},{include:"#quoted-name"}]},number:{patterns:[{captures:{1:{name:"invalid.illegal.dec.edgeql"},2:{name:"invalid.illegal.dec.edgeql"},4:{name:"invalid.illegal.dec.edgeql"},5:{name:"storage.type.number.edgeql"},6:{name:"invalid.illegal.dec.edgeql"},7:{name:"invalid.illegal.dec.edgeql"}},match:`(?x)
  (?:
    #decimal part
    \\.(_*)(?:[0-9](?:[0-9_]*[0-9])?)
    |
    # integer part
    \\b(?: [1-9](?: [0-9_]*[0-9] )? | 0 )
      (?:\\.(_*)[0-9](?:[0-9_]*[0-9])?)
  )
  ((_*)([eE][\\+\\-]?)(_*)[0-9](?:[0-9_]*[0-9])?)?

  (_*)
  \\b
`,name:"constant.numeric.float.edgeql"},{captures:{2:{name:"invalid.illegal.dec.edgeql"},3:{name:"storage.type.number.edgeql"},4:{name:"invalid.illegal.dec.edgeql"},5:{name:"invalid.illegal.dec.edgeql"}},match:`(?x)
  (?:
    # integer part
    \\b(?:[1-9](?:[0-9_]*[0-9])?|0)
  )
  ((_*)([eE][\\+\\-]?)(_*)[0-9](?:[0-9_]*[0-9])?)

  (_*)
  \\b
`,name:"constant.numeric.float.edgeql"},{captures:{1:{name:"invalid.illegal.dec.edgeql"},2:{name:"invalid.illegal.dec.edgeql"},4:{name:"invalid.illegal.dec.edgeql"},5:{name:"storage.type.number.edgeql"},6:{name:"invalid.illegal.dec.edgeql"},7:{name:"invalid.illegal.dec.edgeql"},8:{name:"storage.type.number.edgeql"}},match:`(?x)
  (?:
    #decimal part
    \\.(_*)(?:[0-9](?:[0-9_]*[0-9])?)
    |
    # integer part
    \\b(?:[1-9](?:[0-9_]*[0-9])?|0)
      (?:\\.(_*)[0-9](?:[0-9_]*[0-9])?)?
  )
  ((_*)([eE][\\+\\-]?)(_*)[0-9](?:[0-9_]*[0-9])?)?

  (_*)
  (n)
`,name:"constant.numeric.decimal.edgeql"},{captures:{1:{name:"invalid.illegal.dec.edgeql"},2:{name:"invalid.illegal.dec.edgeql"}},match:`(?x)
  (?:
    # integer part
    \\b(?:[1-9](?:[0-9_]*[0-9])?|0)
  )
  (_*)
  \\b
`,name:"constant.numeric.integer.edgeql"}]},operators:{patterns:[{match:"(\\.[<>])",name:"keyword.operator.navigation.edgeql"},{match:"(::)",name:"keyword.operator.namespace.edgeql"},{match:"->",name:"keyword.declaration.edgeql"},{match:"(:(?!=))",name:"punctuation.declaration.delimiter.edgeql"},{match:`(?x)
  \\?\\!\\= | \\?\\? | \\?\\= | \\>\\= | \\<\\= | \\:\\= | \\/\\/ | \\+\\+ |
  \\!\\= | \\^ | \\> | \\= | \\< | \\/ | \\- | \\+ |
  \\* | \\%
`,name:"keyword.operator.edgeql"}]},parencommon:{patterns:[{match:"(,)",name:"punctuation.separator.element.edgeql"},{match:"(;)",name:"invalid.illegal.delimiter.edgeql"}]},parentheses:{begin:"(\\()",beginCaptures:{1:{name:"punctuation.parenthesis.begin.edgeql"}},end:"(\\))",endCaptures:{1:{name:"punctuation.parenthesis.end.edgeql"}},patterns:[{include:"#expressions"},{include:"#parencommon"}]},"quoted-name":{match:"(`.*?`)",name:"string.interpolated.edgeql"},shape:{begin:"(?=\\S)",end:"(?=\\})",patterns:[{match:"(;)",name:"punctuation.statement.delimiter.edgeql"},{include:"#expressions"},{include:"#parencommon"}]},squarebraces:{begin:"(\\[)",beginCaptures:{1:{name:"punctuation.parenthesis.begin.edgeql"}},end:"(\\])",endCaptures:{1:{name:"punctuation.parenthesis.end.edgeql"}},patterns:[{match:"(^|\\b|\\s)(->)($|\\b|\\s)",name:"keyword.operator.edgeql"},{include:"#expressions"},{include:"#parencommon"}]},string:{patterns:[{begin:`(r)(['"])`,beginCaptures:{1:{name:"storage.type.string.edgeql"},2:{name:"punctuation.definition.string.begin.edgeql"}},end:"(\\2)",endCaptures:{1:{name:"punctuation.definition.string.end.edgeql"}},name:"string.quoted.raw.edgeql"},{begin:`(['"])`,beginCaptures:{1:{name:"punctuation.definition.string.begin.edgeql"}},end:"(\\1)",endCaptures:{1:{name:"punctuation.definition.string.end.edgeql"}},name:"string.quoted.edgeql",patterns:[{include:"#string-escapes"},{include:"#string-invalid-escapes"}]},{begin:"(\\$([[:alpha:]_][[:alnum:]]*)*\\$)",beginCaptures:{1:{name:"punctuation.definition.string.begin.edgeql"}},end:"(\\1)",endCaptures:{1:{name:"punctuation.definition.string.end.edgeql"}},name:"string.dollar.edgeql"}]},"string-escapes":{match:`(?x)
  (
    \\\\(?=\\s*\\n) |
    \\\\[\\\\'"bfnrt] |
    \\\\x[0-7][0-9a-fA-F] |
    \\\\u[0-9a-fA-F]{4} |
    \\\\U[0-9a-fA-F]{8}
  )
`,name:"constant.character.escape.edgeql"},"string-invalid-escapes":{match:"(\\\\.)",name:"invalid.illegal.escapes.edgeql"},types:{patterns:[{begin:"\\b(?<!::|\\.)(tuple)\\s*(<)",beginCaptures:{1:{name:"storage.type.edgeql"},2:{name:"storage.type.placeholder.begin.edgeql"}},end:"(>)",endCaptures:{1:{name:"storage.type.placeholder.end.edgeql"}},patterns:[{include:"#types"},{match:"(,)",name:"punctuation.separator.type.edgeql"},{match:"(:)"},{include:"#types-common"}]},{begin:"\\b(?<!::|\\.)(array)\\s*(<)",beginCaptures:{1:{name:"storage.type.edgeql"},2:{name:"storage.type.placeholder.begin.edgeql"}},end:"(>)",endCaptures:{1:{name:"storage.type.placeholder.end.edgeql"}},patterns:[{match:"array",name:"invalid.illegal.type.edgeql"},{include:"#types"},{include:"#array-dimensions"},{include:"#types-common"}]},{match:`(?x) \\b(?<!::|\\.)(
  array | tuple
)\\b
`,name:"storage.type.edgeql"},{include:"#builtin-modules"},{include:"#builtin-types"},{include:"#builtin-indexes"}]},"types-common":{patterns:[{include:"#identifier"},{match:"(::)",name:"keyword.operator.namespace.edgeql"}]},values:{patterns:[{include:"#number"},{include:"#bytes"},{include:"#string"}]},variables:{begin:"(\\$)(?=[[:alnum:]_]|`)",end:"(?<=[[:alnum:]_`])",name:"constant.language.variable.edgeql",patterns:[{include:"#identifier"},{include:"#quoted-name"},{match:"(\\d)+"}]}},scopeName:"source.edgeql"},n=e;export{n as default};
