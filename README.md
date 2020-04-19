# Terrible-Thick-Client

"Terrible Thick Client" is a vulnerable application developed in C# .NET framework. The vulnerabilities covered so far includes:<br/>
1).Weak cryptography<br/>
2).Lack of code obfuscation<br/>
3).Exposed decryption logic<br/>
4).Sensitive data in memory<br/>
5).Weak implementation of licensing system<br/>

![](TTC.gif)


==> References:<br/>
https://docs.microsoft.com/en-us/windows/win32/api/dpapi/nf-dpapi-cryptprotectdata <br/>
https://gist.github.com/haeky/5797333 <br/>
http://eddiejackson.net/wp/?p=23767 <br/>
https://social.msdn.microsoft.com/Forums/en-US/a23b4ae7-eceb-49f9-bdda-90004567caa4/encrypt-decrypt-data-des-using-dpapi-to-store-key-in-registry?forum=csharplanguage <br/>
