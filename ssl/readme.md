
> https://gist.github.com/dahlsailrunner/679e6dec5fd769f30bce90447ae80081

generate using

`openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout scaling-sample.key -out scaling-sample.crt -config scaling-sample.conf -passin pass:YourStrongPassword`
`openssl pkcs12 -export -out scaling-sample.pfx -inkey scaling-sample.key -in scaling-sample.crt`