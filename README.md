# Modular Monolith Template

![Overview](https://raw.githubusercontent.com/devmentors/modular-monolith-template/master/assets/template.png)

## About

Modular monolith template provides a basic solution for the modular applications. It is based on [modular framework](https://github.com/devmentors/modular-framework) and can be used as a starting point for your next application.

**How to start the solution?**
----------------

Initialize the submodule:

```
git submodule update --init
```

Start the infrastructure using [Docker](https://docs.docker.com/get-docker/):

```
docker-compose up -d
```

Start API located under Bootstrapper project:

```
cd src/Bootstrapper/Modular.Bootstrapper
dotnet run
```