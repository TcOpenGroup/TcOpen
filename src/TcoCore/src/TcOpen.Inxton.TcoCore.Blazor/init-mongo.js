// init-mongo.js
db.createUser({
    user: "admin",
    pwd: "1234",
    roles: [
        {
            role: "readWrite",
            db: "admin",
        },
    ],
});

// Additional initialization commands if needed
ata:
