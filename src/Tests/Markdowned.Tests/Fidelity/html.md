<div align="center" class="x" id="box" style="color:red" onclick="alert(1)">
  <b>bold</b> <i>italic</i> <u>under</u> <font color="red">font</font>
  <a href="https://example.com" target="_blank" class="btn" onclick="x()">link</a>
  <a href="javascript:alert(1)">js</a>
  <a href="#frag">frag</a> <a name="anchor" id="anchor2">named</a>
  <a href="relative/path.md">relative</a> <a href="mailto:a@b.c">mail</a> <a href="ftp://x.y/z">ftp</a>
</div>

<script>alert("x")</script>

<style>body { display: none }</style>

<iframe src="https://example.com"></iframe>

<img src="https://example.com/a.png" width="100" height="50" alt="pic" onerror="x()" style="border:0">

<img src="data:image/png;base64,AAAA" alt="data">

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="https://example.com/dark.png">
  <img alt="logo" src="https://example.com/light.png" width="200">
</picture>

<details>
<summary>Click</summary>

Hidden *markdown*.

</details>

<table>
  <tr><th align="left" colspan="2">H</th></tr>
  <tr><td valign="top" bgcolor="red">a</td><td>b</td></tr>
</table>

<p align="right">right <br> break <sup>sup</sup> <sub>sub</sub> <kbd>K</kbd> <mark>mark</mark></p>

<!-- a comment -->

<unknown-tag attr="1">unknown <span class="s" title="t">span</span></unknown-tag>

<h2 id="custom">Custom id</h2>

Inline <span style="color:red">styled</span> and <script>evil()</script> text and <a href="javascript:x">bad</a>.

<input type="checkbox" disabled> <button onclick="x()">btn</button> <form action="/x"><input name="n"></form>
