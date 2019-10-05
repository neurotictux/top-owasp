## Web application for testing and solutions for OWASP vulnerabilities

### Build docker image
```docker
docker build -t top-owasp:1.0 .
```

### Run image
```docker
docker run -it -p 8080:5000 --name top-owasp top-owasp:1.0 /bin/bash  
```