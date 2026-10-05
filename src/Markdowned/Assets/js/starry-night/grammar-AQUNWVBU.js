import"./grammar-MHCU5XT2.js";var t={dependencies:["etc"],extensions:[],names:["curl-config","curlrc"],patterns:[{include:"#main"}],repository:{auth:{patterns:[{captures:{1:{name:"constant.other.auth-info.curlrc"},2:{patterns:[{include:"etc#kolon"}]},3:{name:"constant.other.auth-info.curlrc"}},match:"([^\\s:;]+)(:)([^\\s=:;]*)"},{captures:{1:{patterns:[{include:"etc#kolon"}]},2:{name:"constant.other.auth-info.curlrc"}},match:"(:)([^\\s:;]*)"}]},authProtocol:{captures:{1:{name:"entity.other.protocol.curlrc"},2:{name:"keyword.operator.protocol.separator.curlrc"}},match:"(?:\\G|^)([^\\\\:\\s/]*)(://|:)"},autoRefer:{captures:{1:{name:"punctuation.separator.key-value.semicolon.curlrc"},2:{name:"variable.assignment.parameter.name.curlrc"}},match:"(;)(auto)\\b"},comment:{begin:'(?:^|(?<=[ \\t\\xA0"]))#',beginCaptures:{0:{name:"punctuation.definition.comment.curlrc"}},end:"$",name:"comment.line.number-sign.curlrc"},header:{captures:{1:{name:"entity.name.header.curlrc"},2:{patterns:[{include:"etc#kolon"}]},3:{name:"string.unquoted.header-value.curlrc"},4:{name:"punctuation.terminator.statement.semicolon.curlrc"}},match:"(?:\\G|^)\\s*([-A-Za-z0-9]+)\\s*(?:(:)\\s*(.*)|(;))"},hexKey:{patterns:[{captures:{1:{name:"keyword.operator.source-modifier.curlrc"},2:{name:"string.unquoted.filename.curlrc"}},match:'(?:^|\\G)\\s*(@)((?:[^"\\s\\\\]|\\\\.)++)'},{captures:{1:{name:"punctuation.definition.string.begin.curlrc"},2:{name:"keyword.operator.source-modifier.curlrc"},3:{name:"string.quoted.double.filename.curlrc"},4:{name:"punctuation.definition.string.end.curlrc"}},match:'(?:^|\\G)\\s*(")(@)((?:[^\\\\"]|\\\\.)++)(")'},{include:"etc#hexNoSign"}]},longOptions:{patterns:[{captures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"}},match:`(?x)
(?:\\G|^|(?<=[ \\t])) \\s*
(
	(--)?
	(?<optlist_no_parameter>
		anyauth
	|	append
	|	basic
	|	ca-native
	|	cert-status
	|	compressed-ssh
	|	compressed
	|	create-dirs
	|	crlf
	|	digest
	|	disable-eprt
	|	disable-epsv
	|	disable
	|	disallow-username-in-url
	|	doh-cert-status
	|	doh-insecure
	|	dump-ca-embed
	|	fail-early
	|	fail-with-body
	|	fail
	|	false-start
	|	follow
	|	form-escape
	|	ftp-create-dirs
	|	ftp-pasv
	|	ftp-pret
	|	ftp-skip-pasv-ip
	|	ftp-ssl-ccc
	|	ftp-ssl-control
	|	get
	|	globoff
	|	haproxy-protocol
	|	head
	|	http0.9
	|	http1.0
	|	http1.1
	|	http2-prior-knowledge
	|	http2
	|	http3-only
	|	http3
	|	ignore-content-length
	|	include
	|	insecure
	|	ipv4
	|	ipv6
	|	junk-session-cookies
	|	list-only
	|	location-trusted
	|	location
	|	mail-rcpt-allowfails
	|	manual
	|	metalink
	|	negotiate
	|	netrc-optional
	|	netrc
	|	next
	|	no-alpn
	|	no-buffer
	|	no-clobber
	|	no-keepalive
	|	no-npn
	|	no-progress-meter
	|	no-sessionid
	|	mptcp
	|	ntlm-wb
	|	ntlm
	|	out-null
	|	parallel-immediate
	|	parallel
	|	path-as-is
	|	post301
	|	post302
	|	post303
	|	progress-bar
	|	proxy-anyauth
	|	proxy-basic
	|	proxy-ca-native
	|	proxy-digest
	|	proxy-http[23]
	|	proxy-insecure
	|	proxy-negotiate
	|	proxy-ntlm
	|	proxy-ssl-allow-beast
	|	proxy-ssl-auto-client-cert
	|	proxy-tlsv1
	|	proxytunnel
	|	raw
	|	remote-header-name
	|	remote-name-all
	|	remote-name
	|	remote-time
	|	remove-on-error
	|	retry-all-errors
	|	retry-connrefused
	|	sasl-ir
	|	show-error
	|	show-headers
	|	silent
	|	skip-existing
	|	socks5-basic
	|	socks5-gssapi-nec
	|	socks5-gssapi
	|	ssl-allow-beast
	|	ssl-auto-client-cert
	|	ssl-no-revoke
	|	ssl-reqd
	|	ssl-revoke-best-effort
	|	sslv2
	|	sslv3
	|	ssl
	|	styled-output
	|	suppress-connect-headers
	|	tcp-fastopen
	|	tcp-nodelay
	|	tftp-no-options
	|	tlsv1.0
	|	tlsv1.1
	|	tlsv1.2
	|	tlsv1.3
	|	tlsv1
	|	tls-earlydata
	|	tr-encoding
	|	trace-ids
	|	trace-time
	|	use-ascii
	|	verbose
	|	version
	|	xattr
	)
)
(?=\\s|$)`,name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:]) \\s*
(
	(--)?
	(?<optlist_urls>
		dns-ipv4-addr
	|	dns-ipv6-addr
	|	dns-servers
	|	doh-url
	|	haproxy-clientip
	|	ipfs-gateway
	|	mail-auth
	|	mail-from
	|	mail-rcpt
	|	noproxy
	|	referer
	|	url
	)
)
(?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|(?=$)))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{patterns:[{include:"#url"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{name:"string.unquoted.curlrc",patterns:[{include:"#url"}]}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:]) \\s*
(
	(--)?
	(?<optlist_headers>
		header
	|	proxy-header
	)
)
(?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|(?=$)))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{patterns:[{include:"#header"},{include:"etc#bareword"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{patterns:[{include:"#header"},{include:"etc#bareword"}]}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:]) \\s*
(
	(--)?
	(?<optlist_hexkey>
		httpsig-key
	)
)
(?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|(?=$)))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{patterns:[{include:"#hexKey"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{patterns:[{include:"#hexKey"}]}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:]) \\s*
(
	(--)?
	(?<optlist_form_data>
		cookie
	|	form-string
	|	form
	|	telnet-option
	|	variable
	)
)
(?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|(?=$)))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{patterns:[{include:"#params"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{patterns:[{include:"#params"}]}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:]) \\s*
(
	(--)?
	(?<optlist_protocols>
		proto-default
	|	proto-redir
	|	proto
	)
) (?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|$))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{patterns:[{include:"#protocols"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{patterns:[{include:"#protocols"}]}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:]) \\s*
(
	(--)?
	(?<optlist_port>
		ftp-port
	)
) (?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|$))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{name:"constant.other.port-address.curlrc",patterns:[{include:"etc#esc"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{name:"constant.other.port-address.curlrc"}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:]) \\s*
(
	(--)?
	(?<optlist_md5>
		hostpubmd5
	)
) (?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|$))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{name:"constant.other.md5.checksum.curlrc",patterns:[{include:"etc#esc"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{name:"constant.other.md5.checksum.curlrc"}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:]) \\s*
(
	(--)?
	(?<optlist_range>
		local-port
	|	range
	)
) (?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|$))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{patterns:[{include:"#range"},{include:"etc#esc"},{include:"etc#bareword"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{patterns:[{include:"#range"},{include:"etc#bareword"}]}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:](?=\\s)) \\s*
(
	(--)?
	(?<optlist_kv_colon>
		aws-sigv4
	|	cert
	|	connect-to
	|	preproxy
	|	proxy-cert
	|	proxy-user
	|	proxy1.0
	|	proxy
	|	resolve
	|	socks4a
	|	socks4
	|	socks5-hostname
	|	socks5
	|	user
	)
)
(?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|$))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{patterns:[{include:"#auth"},{include:"etc#bareword"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{patterns:[{include:"#auth"},{include:"etc#bareword"}]}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:](?=\\s)) \\s*
(
	(--)?
	(?<optlist_string>
		abstract-unix-socket
	|	alt-svc
	|	cacert
	|	capath
	|	cert-type
	|	ciphers
	|	config
	|	cookie-jar
	|	crlfile
	|	curves
	|	data-ascii
	|	data-binary
	|	data-raw
	|	data-urlencode
	|	data
	|	delegation
	|	dns-interface
	|	dump-header
	|	ech
	|	egd-file
	|	engine
	|	etag-compare
	|	etag-save
	|	ftp-account
	|	ftp-alternative-to-user
	|	ftp-method
	|	ftp-ssl-ccc-mode
	|	happy-eyeballs-timeout-ms
	|	help
	|	hostpubsha256
	|	hsts
	|	httpsig-algo
	|	httpsig-headers
	|	httpsig-keyid
	|	interface
	|	json
	|	key-type
	|	key
	|	knownhosts
	|	krb
	|	libcurl
	|	login-options
	|	netrc-file
	|	oauth2-bearer
	|	output-dir
	|	output
	|	pass
	|	pinnedpubkey
	|	proxy-cacert
	|	proxy-capath
	|	proxy-cert-type
	|	proxy-ciphers
	|	proxy-crlfile
	|	proxy-key-type
	|	proxy-key
	|	proxy-pass
	|	proxy-pinnedpubkey
	|	proxy-service-name
	|	proxy-tls13-ciphers
	|	proxy-tlsauthtype
	|	proxy-tlspassword
	|	proxy-tlsuser
	|	pubkey
	|	quote
	|	random-file
	|	request-target
	|	request
	|	sasl-authzid
	|	service-name
	|	sigalgs
	|	socks5-gssapi-service
	|	ssl-sessions
	|	stderr
	|	tls-max
	|	tls13-ciphers
	|	tlsauthtype
	|	tlspassword
	|	tlsuser
	|	trace-ascii
	|	trace-config
	|	trace
	|	unix-socket
	|	upload-file
	|	upload-flags
	|	url-query
	|	user-agent
	|	write-out
	)
)
(?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:(=)?|(?:([-A-Za-z0-9%_]+)(=)?)?([@<]))?(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|$))|([^\\s]+))',endCaptures:{1:{name:"keyword.operator.encoding-modifier.curlrc"},2:{name:"entity.name.form-field.curlrc"},3:{patterns:[{include:"etc#eql"}]},4:{name:"keyword.operator.source-modifier.curlrc"},5:{name:"string.quoted.double.curlrc"},6:{name:"punctuation.definition.string.begin.curlrc"},7:{patterns:[{include:"etc#esc"}]},8:{name:"punctuation.definition.string.end.curlrc"},9:{name:"string.unquoted.curlrc"}},name:"meta.option.long.curlrc"},{begin:`(?x) (?:\\G|^|(?<=[ \\t]))
(?!\\s*--\\w[-\\w]*\\s*[=:])
\\s*
(
	(--)?
	(?<optlist_numeric>
		connect-timeout
	|	continue-at
	|	create-file-mode
	|	expect100-timeout
	|	ip-tos
	|	keepalive-cnt
	|	keepalive-time
	|	limit-rate
	|	max-filesize
	|	max-redirs
	|	max-time
	|	parallel-max-host
	|	parallel-max
	|	rate
	|	retry-delay
	|	retry-max-time
	|	retry
	|	speed-limit
	|	speed-time
	|	tftp-blksize
	|	time-cond
	|	vlan-priority
	)
) (?:\\s*(=|:)|(?=\\s|$))`,beginCaptures:{1:{name:"entity.long.option.name.curlrc"},2:{name:"punctuation.definition.dash.long.option.curlrc"},4:{patterns:[{include:"#separators"}]}},end:'$|(?:((")((?:[^"\\\\]|\\\\.)*)(?:(")|$))|([^\\s]+))',endCaptures:{1:{name:"string.quoted.double.curlrc"},2:{name:"punctuation.definition.string.begin.curlrc"},3:{patterns:[{include:"etc#num"},{include:"etc#bareword"}]},4:{name:"punctuation.definition.string.end.curlrc"},5:{patterns:[{include:"etc#num"},{include:"etc#bareword"}]}},name:"meta.option.long.curlrc"}]},main:{patterns:[{include:"#comment"},{include:"#shortOptions"},{include:"#longOptions"}]},params:{patterns:[{include:"#autoRefer"},{match:'(?:\\G|^|(?<=\\G"|^"))=',name:"keyword.operator.encoding-modifier.curlrc"},{captures:{1:{name:"punctuation.separator.key-value.semicolon.curlrc"},2:{name:"variable.assignment.parameter.name.curlrc"}},match:'(?:\\G|^|(?<=\\G"|^"))(;)([^\\s=;"]+(?="?(?:\\s|$)))?'},{captures:{1:{name:"entity.name.form-field.curlrc"},2:{patterns:[{include:"etc#eql"}]},3:{name:"keyword.operator.source-modifier.curlrc"}},match:'(?:\\G|^|(?<=\\G"|^"))(?:([-A-Za-z0-9%_]+)(=)?)?([@<])'},{captures:{1:{name:"variable.assignment.parameter.name.curlrc"},2:{patterns:[{include:"etc#eql"}]},3:{name:"constant.other.parameter.value.curlrc"},4:{name:"punctuation.separator.key-value.semicolon.curlrc"}},match:"([^\\s=;]+)(=)([^\\s=;]*)(;)?",name:"meta.parameter.curlrc"},{captures:{1:{name:"variable.assignment.parameter.name.curlrc",patterns:[{include:"etc#esc"}]},2:{name:"punctuation.separator.key-value.semicolon.curlrc"}},match:'(?<=@)("(?:[^\\\\"]|\\\\.)++"|(?:[^"\\s;\\\\]|\\\\.)++)(?:(;)|(?=$|\\s))'},{include:"etc#esc"},{include:"etc#bareword"}]},protocols:{patterns:[{match:"[^\\s,+=-]+",name:"constant.other.protocol-name.curlrc"},{match:"\\+",name:"keyword.control.permit-protocol.curlrc"},{match:"-",name:"keyword.control.deny-protocol.curlrc"},{match:"=",name:"keyword.control.permit-protocol.only.curlrc"},{include:"etc#comma"}]},range:{patterns:[{captures:{1:{name:"constant.numeric.integer.int.decimal.dec.range.start.curlrc"},2:{name:"punctuation.separator.range.dash.curlrc"},3:{name:"constant.numeric.integer.int.decimal.dec.range.end.curlrc"},4:{name:"punctuation.separator.range.dash.curlrc"},5:{name:"constant.numeric.integer.int.decimal.dec.range.end.curlrc"}},match:"([0-9]+)(-)([0-9]+)?|(-)([0-9]+)",name:"meta.byte-range.curlrc"},{include:"etc#comma"},{include:"etc#int"}]},separators:{patterns:[{include:"etc#eql"},{include:"etc#kolon"}]},shortOptions:{patterns:[{begin:"^\\s*((-)[:#012346BGIJLMNOQRSVafghijklnpqsv]*[ACDEFHKPQTUXYbcdehmortuwxyz])",beginCaptures:{1:{name:"entity.short.option.name.curlrc"},2:{name:"punctuation.definition.dash.short.option.curlrc"}},end:`(?x)
$
|

# Numbers
(?<=(?#optlist_numeric)[CYmyz])
\\G (?:(?!\xA0)\\s)*
([-+]?[0-9.]+)

|

# Byte range
(?<=(?#optlist_range)r)
\\G (?:(?!\xA0)\\s)*
([-0-9,]+)

|

# \u201Ckey=value\u201D pairs
(?<=(?#optlist_form_data)[Fbt])
\\G (?:(?!\xA0)\\s)*
(?:
	((")((?:[^"\\\\]|\\\\.)*)(?:(")|$))
	|
	([\\S\xA0]+)
)

|

# \u201Ckey:value\u201D pairs
(?<=(?#optlist_kv_colon)[EUux])
\\G (?:(?!\xA0)\\s)*
((?:[^\\\\:\\s/]|\xA0)*://)?
(
	(?:[^\\\\:\\s]|\\\\.|\xA0)+
	(?::(?:[^\\\\:\\s]|\\\\.|\xA0)+)*
	:?
)

|

# Headers
(?<=(?#optlist_headers)H)
\\G (?:(?!\xA0)\\s)*
(?:
	((")((?:[^"\\\\]|\\\\.|\xA0)*)(?:(")|$))
	|
	([\\S\xA0]+)
)

|

# URLs
(?<=(?#optlist_urls)e)
\\G (?:(?!\xA0)\\s)*
(?:
	((")((?:[^"\\\\]|\\\\.|\xA0)*)(?:(")|$))
	|
	([\\S\xA0]+)
)

|

# Port address
(?<=(?#optlist_port)P)
\\G (?:(?!\xA0)\\s)*
(?:
	((")((?:[^"\\\\]|\\\\.|\xA0)*)(?:(")|$))
	|
	([\\S\xA0]+)
)

|

# Anything else
(?:
	((")((?:[^"\\\\]|\\\\.|\xA0)*)(?:(")|$))
	|
	([\\S\xA0]+)
)`,endCaptures:{1:{patterns:[{include:"etc#num"}]},10:{name:"meta.http-headers.curlrc"},11:{name:"punctuation.definition.string.begin.curlrc"},12:{patterns:[{include:"#header"}]},13:{name:"punctuation.definition.string.end.curlrc"},14:{patterns:[{include:"#header"}]},15:{name:"meta.url-string.curlrc"},16:{name:"punctuation.definition.string.begin.curlrc"},17:{patterns:[{include:"#url"}]},18:{name:"punctuation.definition.string.end.curlrc"},19:{patterns:[{include:"#url"}]},2:{patterns:[{include:"#range"}]},20:{name:"string.quoted.double.curlrc"},21:{name:"punctuation.definition.string.begin.curlrc"},22:{name:"constant.other.port-address.curlrc",patterns:[{include:"etc#esc"}]},23:{name:"punctuation.definition.string.end.curlrc"},24:{name:"constant.other.port-address.curlrc"},25:{name:"string.quoted.double.curlrc"},26:{name:"punctuation.definition.string.begin.curlrc"},27:{patterns:[{include:"etc#esc"}]},28:{name:"punctuation.definition.string.end.curlrc"},29:{name:"string.unquoted.curlrc"},3:{name:"meta.parameter-string.curlrc"},4:{name:"punctuation.definition.string.begin.curlrc"},5:{patterns:[{include:"#params"}]},6:{name:"punctuation.definition.string.end.curlrc"},7:{patterns:[{include:"#params"}]},8:{patterns:[{include:"#authProtocol"}]},9:{patterns:[{include:"#auth"}]}},name:"meta.option.short.curlrc"},{captures:{1:{name:"entity.short.option.name.curlrc"},2:{name:"punctuation.definition.dash.short.option.curlrc"}},match:"^\\s*((-)(?#optlist_no_parameter)[#012346:BGIJLMNORSVZafgijklnpqsv]+)",name:"meta.option.short.curlrc"}]},string:{begin:'"',beginCaptures:{0:{name:"punctuation.definition.string.begin.curlrc"}},end:'"|(?=$)',endCaptures:{0:{name:"punctuation.definition.string.end.curlrc"}},name:"string.quoted.double.curlrc",patterns:[{captures:{1:{name:"punctuation.definition.escape.backslash.curlrc"}},match:'(\\\\)[\\\\"tnrv]',name:"constant.character.escape.backslash.curlrc"}]},url:{patterns:[{include:"#autoRefer"},{include:"etc#comma"},{captures:{1:{patterns:[{include:"etc#url"},{include:"#urlNoSchema"}]},2:{patterns:[{include:"etc#url"},{include:"#urlNoSchema"}]}},match:'(?<=\\G"|^")((?:[^"\\\\]|\\\\.)*)(?=$|"|;)|(?:\\G(?<!")|^)([^\\s,]+?)(?=$|\\s|;|,)'},{include:"#params"},{include:"etc#bareword"}]},urlNoSchema:{captures:{1:{name:"constant.other.reference.link.underline.url.curlrc"}},match:"(?:\\G|^)\\s*([-a-zA-Z0-9]+(?:\\.|@)[-a-zA-Z0-9]+.*)\\s*"}},scopeName:"source.curlrc"},n=t;export{n as default};
