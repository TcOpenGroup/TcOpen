#Base Image for MongoDB
FROM mongo:5.0.3

#MongoDB dump gets copied from TcIoen.Hammer/Mongo/dump
# to location in Docker /dump
Copy dump/ /dump/

#i copied it from the Inet and tweaked it
# mongorestore ist the important command, which restores the existing DB "Hammer"
CMD mongod --fork --logpath /var/log/mongodb.log; \
    mongorestore --db Hammer /dump/Hammer; \
    mongod --shutdown; \
    docker-entrypoint.sh mongod;
