# RavenDB Repository

This is an implementation of data persistence via RavenDB.

## What is RavenDB?

[RavenDB](https://github.com/ravendb/ravendb) is an Open Source ACID NoSQL database. 

It started in 2009 as a document database, but over time included many ways to model data, so you will be able to accomodate various business and industry scenarios.

You will be able to use it a fast CRUD engine to [read](https://ravendb.net/docs/article-page/latest/csharp/client-api/session/loading-entities) and [write](https://ravendb.net/docs/article-page/latest/csharp/client-api/session/storing-entities) JSON as a native format, but also to manipulate and index JSON via [Map](https://ravendb.net/docs/article-page/latest/csharp/indexes/map-indexes) and [Map/Reduce](https://ravendb.net/docs/article-page/latest/csharp/indexes/map-reduce-indexes) indexes which are powering fast Queries even with [multi-terabyte datasets](https://ravendb.net/whitepapers/couchbase-vs-ravendb-performance-at-rakuten-kobo).

## How to install it?

### On-premise

You can [install](https://ravendb.net/docs/article-page/latest/csharp/start/installation/setup-wizard) and maintain yourself a single node or multinode cluster. Follow [instructions](https://ravendb.net/docs/article-page/latest/csharp/start/installation/setup-wizard) to easily set up secured instance of RavenDB.

[Various options](https://ravendb.net/download) are at your disposal
- Native installation on Linux and Windows
- Raspberry Pi
- Docker
- Kubernetes
- ARM processors

but [Dockerized](https://ravendb.net/docs/article-page/latest/csharp/start/installation/running-in-docker-container) one is probably most straightforward one

```
docker run -d -p 8080:8080 -p 38888:38888 -e RAVEN_ARGS="--Setup.Mode=None --License.Eula.Accepted=true" ravendb/ravendb
```

and RavenDB instance will be available in a few moments at

```
http://127.0.0.1:8080/
```

### In the cloud

RavenDB offers its own Database-as-a-Service (DBaaS) [RavenDB Cloud](https://cloud.ravendb.net/) where you can create cluster residing on AWS, Azure or GCP infrastructure. With this option, you can concentrate on developing your application while [complete DevOps](https://ravendb.net/docs/article-page/latest/csharp/cloud/cloud-overview) including automated offsite backups will be handled by RavenDB DevOps team.

If your project is a low-demand one, there is a [Free 1-node cluster](https://ravendb.net/docs/article-page/latest/csharp/cloud/cloud-instances#a-free-cloud-node) available.

## What about the license?

### Open Source projects

RavenDB is licensed under AGPLv3 license, so you are free to use it with your open source project.

### Commercial projects

During development, [free Developer license](https://ravendb.net/buy#developer) is available (just register with your email address).

Once you go into production, [free Community license](https://ravendb.net/license/request/community) is suitable for commercial usage.

However, if you decide to go with [RavenDB Cloud](https://cloud.ravendb.net/) your Enterprise license will be provided out of the box with every instance or cluster you create.