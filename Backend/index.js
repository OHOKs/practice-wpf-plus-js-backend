const express = require("express");
const bodyParser = require("body-parser")
const cors = require("cors");

const userRouter = require('./routes/user.routes');

const app = express();

const corsOptions = {
    origin: 'http://localhost:5173',
    credentials: true, 
};

app.use(express.json()); 
app.use(bodyParser.urlencoded({ extended: true }));
app.use(cors(corsOptions));

app.use('/api', userRouter); 


const PORT = process.env.PORT || 3000;
app.listen(PORT,() => {
    console.log(`Szerver fut a http://localhost:${PORT} címen`);
});