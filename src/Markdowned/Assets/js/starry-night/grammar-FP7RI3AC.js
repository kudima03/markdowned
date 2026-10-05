import"./grammar-MHCU5XT2.js";var e={dependencies:["source.sql"],extensions:[".cfc"],names:["coldfusion-cfc","cfc"],patterns:[{include:"#comments"},{include:"#cfcomments"},{include:"#component-operators"},{include:"#functions"},{include:"#tag-operators"},{include:"#cfscript-code"}],repository:{braces:{patterns:[{match:"{|}",name:"meta.brace.curly.cfscript"},{match:"\\(|\\)",name:"meta.brace.round.cfscript"},{begin:"([\\w]+)?\\s*(\\[)",beginCaptures:{1:{name:"variable.other.set.cfscript"},2:{name:"punctuation.definition.set.begin.cfscript"}},end:"\\]",endCaptures:{0:{name:"punctuation.definition.set.end.cfscript"}},patterns:[{include:"#strings"},{match:",",name:"punctuation.definition.set.seperator.cfscript"},{include:"$self"}]}]},cfcomments:{patterns:[{match:"<!---.*--->",name:"comment.line.cfml"},{begin:"<!---",captures:{0:{name:"punctuation.definition.comment.cfml"}},end:"--->",name:"comment.block.cfml",patterns:[{include:"#cfcomments"}]}]},"cfscript-code":{patterns:[{include:"#braces"},{include:"#closures"},{include:"#sql-code"},{include:"#keywords"},{include:"#function-call"},{include:"#constants"},{include:"#variables"},{include:"#strings"}]},closures:{begin:"(?i:\\b(function))\\b",beginCaptures:{1:{name:"storage.closure.cfscript"}},end:"(?={)",name:"meta.closure.cfscript",patterns:[{include:"#parameters"}]},"comment-block":{begin:"/\\*",captures:{0:{name:"punctuation.definition.comment.cfscript"}},end:"\\*/",name:"comment.block.cfscript"},comments:{patterns:[{captures:{0:{name:"punctuation.definition.comment.cfscript"}},match:"/\\*\\*/",name:"comment.block.empty.cfscript"},{include:"text.html.javadoc"},{include:"#comment-block"},{captures:{1:{name:"comment.line.double-slash.cfscript"},2:{name:"punctuation.definition.comment.cfscript"}},match:"((//).*?[^\\s])\\s*$\\n?"}]},"component-extends-attribute":{begin:'\\b(extends)\\b\\s*(=)\\s*(?=")',captures:{1:{name:"entity.name.tag.operator-attribute.extends.cfml"},2:{name:"keyword.operator.assignment.cfscript"}},end:"(?=[\\s{])",name:"meta.component.attribute-with-value.extends.cfml",patterns:[{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin.cfscript"}},contentName:"meta.component-operator.extends.value.cfscript",end:'"',endCaptures:{0:{name:"punctuation.definition.string.end.cfscript"}},name:"string.quoted.double.cfml"},{begin:"'",beginCaptures:{0:{name:"punctuation.definition.string.begin.cfscript"}},contentName:"meta.component-operator.extends.value.cfscript",end:"'",endCaptures:{0:{name:"punctuation.definition.string.end.cfscript"}},name:"string.quoted.single.cfscript"}]},"component-operators":{patterns:[{begin:`(?x)
                        \\b
                        (?i:
                        (component)
                        )
                        \\b
                        \\s+
                        (?![\\.\\/>=,#\\)])
                    `,beginCaptures:{1:{name:"entity.name.tag.operator.component.cfscript"}},end:"(?=[;{\\(])",name:"meta.operator.cfscript meta.class.component.cfscript",patterns:[{include:"#component-extends-attribute"},{match:"(?i:(\\w+)\\s*(?=\\=))",name:"entity.other.attribute-name.cfscript"},{include:"#cfscript-code"}]}]},constants:{patterns:[{match:`(?x)(
                            (\\b[0-9]+)
                            |
                            (\\.[0-9]+[0-9\\.]*) # decimals
                            |
                            (0(x|X)[0-9a-fA-F]+) # hex
                            # matches really large double/floats
                            |(\\.[0-9]+)((e|E)(\\+|-)?[0-9]+)?([LlFfUuDd]|UL|ul)?
                            )\\b
                        `,name:"constant.numeric.cfscript"},{match:"\\b(?i:(true|false|null))\\b",name:"constant.language.cfscript"},{match:"\\b_?([A-Z][A-Z0-9_]+)\\b",name:"constant.other.cfscript"}]},"function-call":{begin:`(?x)
                (?i:
                    (abs|acos|addsoaprequestheader|addsoapresponseheader|ajaxlink|ajaxonload|applicationstop
                    |arrayappend|arrayavg|arrayclear|arraycontains|arraydelete|arraydeleteat
                    |arrayfind|arrayfindnocase|arrayinsertat|arrayisdefined|arrayisempty|arraylen
                    |arraymax|arraymin|arraynew|arrayprepend|arrayresize|arrayset|arraysort|arraysum
                    |arrayswap|arraytolist|asc|asin|atn|authenticatedcontext|authenticateduser|binarydecode
                    |binaryencode|bitand|bitmaskclear|bitmaskread|bitmaskset|bitnot|bitor|bitshln|bitshrn
                    |bitxor|cacheget|cachegetallids|cachegetmetadata|cachegetproperties|cachegetsession
                    |cacheput|cacheremove|cachesetproperties|ceiling|charsetdecode|charsetencode|chr
                    |cjustify|compare|comparenocase|cos|createdate|createdatetime|createobject|createodbcdate
                    |createodbcdatetime|createodbctime|createtime|createtimespan|createuuid|dateadd|datecompare
                    |dateconvert|datediff|dateformat|datepart|day|dayofweek|dayofweekasstring|dayofyear
                    |daysinmonth|daysinyear|decimalformat|decrementvalue|decrypt|decryptbinary
                    |deleteclientvariable|deserializejson|de|directorycreate|directorydelete|directoryexists
                    |directorylist|directoryrename|dollarformat|dotnettocftype|duplicate|encrypt|encryptbinary
                    |entitydelete|entityload|entityloadbyexample|entityloadbypk|entitymerge|entitynew
                    |entityreload|entitysave|entitytoquery|evaluate|exp|expandpath|fileclose|filecopy
                    |filedelete|fileexists|fileiseof|filemove|fileopen|fileread|filereadbinary|filereadline
                    |fileseek|filesetaccessmode|filesetattribute|filesetlastmodified|fileskipbytes|fileupload
                    |fileuploadall|filewrite|filewriteline|find|findnocase|findoneof|firstdayofmonth|fix
                    |formatbasen|generatesecretkey|getauthuser|getbasetagdata|getbasetaglist|getbasetemplatepath
                    |getclientvariableslist|getcomponentmetadata|getcontextroot|getcurrenttemplatepath
                    |getdirectoryfrompath|getencoding|getexception|getfilefrompath|getfileinfo
                    |getfunctioncalledname|getfunctionlist|getgatewayhelper|gethttprequestdata|gethttptimestring
                    |getk2serverdoccount|getk2serverdoccountlimit|getlocale|getlocaledisplayname|getlocalhostip
                    |getmetadata|getmetricdata|getpagecontext|getrequest|getrequesturi|getprinterinfo|getprinterlist|getprofilesections
                    |getprofilestring|getreadableimageformats|getsoaprequest|getsoaprequestheader|getsoapresponse
                    |getsoapresponseheader|gettempdirectory|gettempfile|gettemplatepath|gettickcount
                    |gettimezoneinfo|gettoken|getuserroles|getvfsmetadata|getwriteableimageformats|hash|hour
                    |htmlcodeformat|htmleditformat|iif|imageaddborder|imageblur|imageclearrect|imagecopy
                    |imagecrop|imagedrawarc|imagedrawbeveledrect|imagedrawcubiccurve|imagedrawline|imagedrawlines
                    |imagedrawoval|imagedrawpoint|imagedrawquadraticcurve|imagedrawrect|imagedrawroundrect
                    |imagedrawtext|imageflip|imagegetblob|imagegetbufferedimage|imagegetexifmetadata|imagegetexiftag
                    |imagegetheight|imagegetiptcmetadata|imagegetiptctag|imagegetwidth|imagegrayscale|imageinfo
                    |imagenegative|imagenew|imageoverlay|imagepaste|imageread|imagereadbase64|imageresize
                    |imagerotate|imagerotatedrawingaxis|imagescaletofit|imagesetantialiasing|imagesetbackgroundcolor
                    |imagesetdrawingcolor|imagesetdrawingstroke|imagesetdrawingtransparency|imagesharpen|imageshear
                    |imagesheardrawingaxis|imagetranslate|imagetranslatedrawingaxis|imagewrite|imagewritebase64
                    |imagexordrawingmode|incrementvalue|inputbasen|insert|int|isarray|isauthenticated|isauthorized
                    |isbinary|isboolean|iscustomfunction|isdate|isddx|isdebugmode|isdefined|isimage|isimagefile
                    |isinstanceof|isipv6|isjson|isk2serverabroker|isk2serverdoccountexceeded|isk2serveronline|isleapyear
                    |islocalhost|isnull|isnumeric|isnumericdate|isobject|ispdffile|ispdfobject|isprotected|isquery
                    |issimplevalue|issoaprequest|isspreadsheetfile|isspreadsheetobject|isstruct|isuserinanyrole
                    |isuserinrole|isuserloggedin|isvalid|iswddx|isxml|isxmlattribute|isxmldoc|isxmlelem|isxmlnode
                    |isxmlroot|javacast|jsstringformat|lcase|left|len|listappend|listchangedelims|listcontains
                    |listcontainsnocase|listdeleteat|listfind|listfindnocase|listfirst|listgetat|listinsertat
                    |listlast|listlen|listprepend|listqualify|listrest|listsetat|listsort|listtoarray|listvaluecount
                    |listvaluecountnocase|ljustify|location|log|log10|lscurrencyformat|lsdateformat|lseurocurrencyformat
                    |lsiscurrency|lsisdate|lsisnumeric|lsnumberformat|lsparsecurrency|lsparsedatetime|lsparseeurocurrency
                    |lsparsenumber|lstimeformat|ltrim|max|mid|min|minute|month|monthasstring|now|numberformat|objectequals
                    |objectload|objectsave|ormclearsession|ormclosesession|ormcloseallsessions|ormevictcollection
                    |ormevictentity|ormevictqueries|ormexecutequery|ormflush|ormflushall|ormgetsession|ormgetsessionfactory
                    |ormreload|paragraphformat|parameterexists|parsedatetime|pi|precisionevaluate|preservesinglequotes
                    |quarter|queryaddcolumn|queryaddrow|queryconvertforgrid|querynew|querysetcell|quotedvaluelist
                    |rand|randomize|randrange|refind|refindnocase|rematch|rematchnocase|releasecomobject|removechars
                    |repeatstring|replace|replacelist|replacenocase|rereplace|rereplacenocase|reverse|right|rjustify
                    |round|rtrim|second|sendgatewaymessage|serializejson|setencoding|setlocale|setprofilestring
                    |setvariable|sgn|sin|sleep|spanexcluding|spanincluding|spreadsheetaddcolumn|spreadsheetaddimage
                    |spreadsheetaddfreezepane|spreadsheetaddinfo|spreadsheetaddrow|spreadsheetaddrows|spreadsheetaddsplitpane
                    |spreadsheetcreatesheet|spreadsheetdeletecolumn|spreadsheetdeletecolumns|spreadsheetdeleterow
                    |spreadsheetdeleterows|spreadsheetformatcell|spreadsheetformatcolumn|spreadsheetformatcellrange
                    |spreadsheetformatcolumns|spreadsheetformatrow|spreadsheetformatrows|spreadsheetgetcellcomment
                    |spreadsheetgetcellformula|spreadsheetgetcellvalue|spreadsheetinfo|spreadsheetmergecells
                    |spreadsheetnew|spreadsheetread|spreadsheetreadbinary|spreadsheetremovesheet|spreadsheetsetactivesheet
                    |spreadsheetsetactivesheetnumber|spreadsheetsetcellcomment|spreadsheetsetcellformula|spreadsheetsetcellvalue
                    |spreadsheetsetcolumnwidth|spreadsheetsetfooter|spreadsheetsetheader|spreadsheetsetrowheight
                    |spreadsheetshiftcolumnsspreadsheetshiftrows|spreadsheetwrite|sqr|stripcr|structappend|structclear
                    |structcopy|structcount|structdelete|structfind|structfindkey|structfindvalue|structget|structinsert
                    |structisempty|structkeyarray|structkeyexists|structkeylist|structnew|structsort|structupdate|tan
                    |threadjoin|threadterminate|throw|timeformat|tobase64|tobinary|toscript|tostring|trace|transactioncommit
                    |transactionrollback|transactionsetsavepoint|trim|ucase|urldecode|urlencodedformat|urlsessionformat
                    |val|valuelist|verifyclient|week|wrap|writedump|writelog|writeoutput|xmlchildpos|xmlelemnew
                    |xmlformat|xmlgetnodetype|xmlnew|xmlparse|xmlsearch|xmltransform|xmlvalidate|year|yesnoformat)
                    |
                    (\\w+)
                )
                \\s*
                (\\()
            `,beginCaptures:{1:{name:"support.function.cfscript"},2:{name:"entity.name.function-call.cfscript"},3:{name:"punctuation.definition.arguments.begin.cfscript"}},end:"(\\))",endCaptures:{1:{name:"punctuation.definition.arguments.end.cfscript"}},name:"meta.function-call.cfscript",patterns:[{match:",",name:"punctuation.definition.seperator.arguments.cfscript"},{match:"(?i:(\\w+)\\s*(?=\\=))",name:"entity.other.method-parameter.cfscript"},{include:"#cfcomments"},{include:"#comments"},{include:"#tag-operators"},{include:"#cfscript-code"}]},"function-properties":{patterns:[{match:"\\b(?i:output)",name:"entity.other.attribute-name.output.cfscript"},{match:"\\b([\\w]+)",name:"entity.other.attribute-name.any.cfscript"}]},functions:{begin:`(?x)^\\s*
                    (?:
                        (?: # optional access-control modifier and return-type
                            (?i:\\b(private|package|public|remote)\\s+)? # access-control.modifier
                            (?i:\\b
                                (void)
                                |
                                (any|array|binary|boolean|component|date|guid|numeric|query|string|struct|xml|uuid) # return-type.primitive
                                |
                                ([A-Za-z0-9_\\.$]+) #return-type component/object (may need additional tokens)
                            )?
                        )
                    )
                    \\s*
                    (?i:(function)) # storage.function
                    \\s+
                    (?:
                        (init) # entity.name.function.contructor
                        |
                        ([\\w\\$]+) # entity.name.function
                    )\\b
                `,beginCaptures:{1:{name:"storage.modifier.access-control.cfscript"},2:{name:"storage.type.return-type.void.cfscript"},3:{name:"storage.type.return-type.primitive.cfscript"},4:{name:"storage.type.return-type.object.cfscript"},5:{name:"storage.type.function.cfscript"},6:{name:"entity.name.function.constructor.cfscript"},7:{name:"entity.name.function.cfscript"}},end:"(?={)",name:"meta.function.cfscript",patterns:[{include:"#parameters"},{include:"#comments"},{include:"#function-properties"},{include:"#cfscript-code"}]},keywords:{patterns:[{match:"\\b(?i:new)\\b",name:"keyword.other.new.cfscript"},{match:"(===?|!|!=|<=|>=|<|>)",name:"keyword.operator.comparison.cfscript"},{match:"\\b(?i:(GREATER|LESS|THAN|EQUAL\\s+TO|DOES|CONTAINS|EQUAL|EQ|NEQ|LT|LTE|LE|GT|GTE|GE|AND|IS))\\b",name:"keyword.operator.decision.cfscript"},{match:"(\\-\\-|\\+\\+)",name:"keyword.operator.increment-decrement.cfscript"},{match:"(?i:(\\^|\\-|\\+|\\*|\\/|\\\\|%|\\-=|\\+=|\\*=|\\/=|%=|\\bMOD\\b))",name:"keyword.operator.arithmetic.cfscript"},{match:"(&|&=)",name:"keyword.operator.concat.cfscript"},{match:"(=)",name:"keyword.operator.assignment.cfscript"},{match:"\\b(?i:(NOT|!|AND|&&|OR|\\|\\||XOR|EQV|IMP))\\b",name:"keyword.operator.logical.cfscript"},{match:"(\\?|:)",name:"keyword.operator.ternary.cfscript"},{match:";",name:"punctuation.terminator.cfscript"}]},nest_hash:{patterns:[{match:"##",name:"string.escaped.hash.cfscript"},{begin:"(#)(?=.*#)",beginCaptures:{1:{name:"punctuation.definition.hash.begin.cfscript"}},contentName:"source.cfscript.embedded.cfscript",end:"(#)",endCaptures:{1:{name:"punctuation.definition.hash.end.cfscript"}},name:"meta.inline.hash.cfscript",patterns:[{include:"#cfscript-code"}]}]},parameters:{patterns:[{begin:"(\\()",beginCaptures:{1:{name:"punctuation.definition.parameters.begin.cfscript"}},end:"(\\))",endCaptures:{1:{name:"punctuation.definition.parameters.end.cfscript"}},name:"meta.function.parameters.cfscript",patterns:[{match:"(?i:required)",name:"keyword.other.required.argument.cfscript"},{include:"#storage-types"},{match:"(=)",name:"keyword.operator.argument-assignment.cfscript"},{match:"(?i:false|true|no|yes)",name:"constant.language.boolean.argument.cfscript"},{match:"(?i:\\w)",name:"variable.parameter.cfscript"},{match:",",name:"punctuation.definition.seperator.parameter.cfscript"},{include:"#strings"}]}]},"sql-code":{patterns:[{begin:`([\\w+\\.]+)\\.((?i:setsql))\\(\\s*["|']`,beginCaptures:{1:{name:"entity.name.function.query.cfscript, meta.toc-list.query.cfscript"},2:{name:"support.function.cfscript"}},end:`(["|']\\s*\\))`,endCaptures:{1:{name:"punctuation.parenthesis.end.cfscript"}},name:"source.sql.embedded.cfscript",patterns:[{include:"#nest_hash"},{include:"source.sql"}]}]},"storage-types":{patterns:[{match:"\\b(?i:(function|string|date|struct|array|void|binary|numeric|boolean|query|xml|uuid|any))\\b",name:"storage.type.primitive.cfscript"}]},"string-quoted-double":{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin.cfscript"}},end:'"(?!")',endCaptures:{0:{name:"punctuation.definition.string.end.cfscript"}},name:"string.quoted.double.cfscript",patterns:[{match:'("")',name:"constant.character.escape.quoted.double.cfscript"},{include:"#nest_hash"}]},"string-quoted-single":{begin:"'",beginCaptures:{0:{name:"punctuation.definition.string.begin.cfscript"}},end:"'(?!')",endCaptures:{0:{name:"punctuation.definition.string.end.cfscript"}},name:"string.quoted.single.cfscript",patterns:[{match:"('')",name:"constant.character.escape.quoted.single.cfscript"},{include:"#nest_hash"}]},strings:{patterns:[{include:"#string-quoted-double"},{include:"#string-quoted-single"}]},"tag-operators":{patterns:[{match:"\\b(else\\s+if|else|if)\\b",name:"keyword.control.operator.conditional.cfscript"},{match:"\\b(switch|case|default)\\b",name:"keyword.control.operator.switch.cfscript"},{begin:`(?x)^[\\s}]*
                        (?i:
                        (lock)|(transaction)|(thread)|(abort)
                        |(exit)|(include)|(param)|(thread)|(import)
                        |(rethrow|throw)|(property)|(interface)|(location)
                        |(break)|(pageencoding)|(schedule)|(return)|(try|catch|finally)
                        |(for|in|do|while|break|continue)
                        |(trace)|(savecontent)|(http|httpparam)
                        )
                        \\b
                        \\s*
                        (?![^\\w|"|'|\\(|{|;])
                    `,beginCaptures:{1:{name:"entity.name.tag.operator.lock.cfscript"},10:{name:"keyword.control.operator.catch-exception.cfscript"},11:{name:"entity.name.tag.operator.property.cfscript"},12:{name:"entity.name.tag.operator.interface.cfscript"},13:{name:"entity.name.tag.operator.location.cfscript"},14:{name:"keyword.control.operator.break.cfscript"},15:{name:"entity.name.tag.operator.pageencoding.cfscript"},16:{name:"entity.name.tag.operator.schedule.cfscript"},17:{name:"keyword.control.operator.return.cfscript"},18:{name:"keyword.control.operator.catch-exception.cfscript"},19:{name:"keyword.control.operator.loop.cfscript"},2:{name:"entity.name.tag.operator.transaction.cfscript"},20:{name:"entity.name.tag.operator.trace.cfscript"},21:{name:"entity.name.tag.operator.savecontent.cfscript"},22:{name:"entity.name.tag.operator.http.cfscript"},3:{name:"entity.name.tag.operator.thread.cfscript"},4:{name:"keyword.control.operator.abort.cfscript"},5:{name:"keyword.control.operator.exit.cfscript"},6:{name:"entity.name.tag.operator.include.cfscript"},7:{name:"entity.name.tag.operator.param.cfscript"},8:{name:"entity.name.tag.operator.thread.cfscript"},9:{name:"entity.name.tag.operator.import.cfscript"}},end:"(;)|({)",endCaptures:{1:{name:"punctuation.terminator.cfscript"},3:{name:"meta.brace.curly.cfscript"}},name:"meta.operator.cfscript",patterns:[{begin:"\\(",beginCaptures:{0:{name:"meta.brace.curly.cfscript"}},end:"\\)",endCaptures:{0:{name:"meta.brace.curly.cfscript"}},patterns:[{match:",",name:"punctuation.definition.seperator.arguments.cfscript"},{match:"(?i:(\\w+)\\s*(?=\\=))",name:"entity.other.operator-parameter.cfscript"},{include:"#cfscript-code"}]},{match:"(?i:(\\w+)\\s*(?=\\=))",name:"entity.other.attribute-name.cfscript"},{include:"#cfcomments"},{include:"#comments"},{include:"#cfscript-code"}]}]},variables:{patterns:[{match:"\\b(?i:var)\\b",name:"storage.modifier.var.cfscript"},{match:"\\b(?i:(this|key))(?!\\.)",name:"variable.language.cfscript"},{match:"(\\.)",name:"punctuation.definition.seperator.variable.cfscript"},{captures:{1:{name:"variable.language.cfscript"},2:{name:"variable.other.cfscript"}},match:`(?x)
                    (?i:
                        \\b
                        (application|arguments|attributes|caller|cgi|client|
                            cookie|flash|form|local|request|server|session|
                            this|thistag|thread|thread local|url|variables|
                            super|self|argumentcollection)
                        \\b
                        |
                        (\\w+)
                    )`}]}},scopeName:"source.cfscript"},t=e;export{t as default};
