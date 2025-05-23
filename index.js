import express from "express";
import bodyParser from "body-parser";
import pg from "pg";
import bcrypt from "bcrypt";
import path from 'path';
import { fileURLToPath } from 'url';
import { dirname } from 'path';
import env from "dotenv";
import { name } from "ejs";

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

const app = express();
const port = 3000;
const saltRounds = 10;

let teacherName = "";
let studentName = "";
let clientUsername = "";

// app.set('view engine', 'ejs');
// app.set('views', path.join(__dirname, 'views'));

app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.urlencoded({ extended: true })); 
app.use(express.json())
app.use(express.static(path.join(__dirname, 'Invent copy')));

const db = new pg.Client({
  user: "postgres",
  host: "localhost",
  database: "postgres",
  password: "aayan",
  port: 5432,
});
db.connect();

app.get('/', (req, res) => {
  res.render('home.ejs');
});

app.get("/", (req, res) => {
  res.fil===
})

//TEACHER REGISTER AND POST
app.post("/teacher/register", async (req, res) => {
  const username = req.body.username;
  const password = req.body.password;
  const name = req.body.name;

  console.log(username, password, name);

  const hashedPassword = await bcrypt.hash(password, saltRounds);
  const query = `INSERT INTO teacherusers (username, password, name, assigned) VALUES ($1, $2, $3, $4)`;
  const values = [username, hashedPassword, name, "0,0"];

  db.query(query, values, (err, result) => {
    if (err) {
      console.error("Error inserting data:", err);
      res.status(500).send("Error inserting data, this username might already exist.");
    } else {
      console.log("Data inserted successfully");
      teacherName = name;
      clientUsername = username;
      res.redirect("/teacher/dashboard");
    }
  });
});

app.get("/teacher/register", (req, res) => {
  res.render("teacherRegister.ejs");
});
//------------------------

//TEACHER AND STUDENT LOGIN
app.get("/teacher/login", (req, res) => {
  res.render("teacherLogin.ejs");
});
app.post("/teacher/login", (req, res) => {
  const username = req.body.username;
  const password = req.body.password;

  console.log("TEACHER");
  console.log(username, password);

  const query = `SELECT * FROM teacherusers WHERE username = $1`;
  const values = [username];

  //This part is written by AI
  db.query(query, values, (err, result) => {
    if (err) {
      console.error("Error querying data:", err);
      res.status(500).send("Error querying data");
    } else {
      if (result.rows.length > 0) {
        const user = result.rows[0];
        bcrypt.compare(password, user.password, (err, match) => {
          if (match) {
            teacherName = user.name;
            clientUsername = username;
            res.redirect("/teacher/dashboard");
          } else {
            res.status(401).send("Invalid credentials");
          }
        });
      } else {
        res.status(401).send("Invalid credentials");
      }
    }
  });
});

app.get("/student/login", (req, res) => {
  res.render("studentLogin.ejs", { studentN: studentName });
});

app.post("/student/login", (req, res) => {
  const username = req.body.username;
  const password = req.body.password;
  console.log(username, password);
  const query = `SELECT * FROM studentusers WHERE username = $1`;
  const values = [username];
  console.log(username, password);
  db.query(query, values, (err, result) => {
    if (err) {
      console.error("Error querying data:", err);
      res.status(500).send("Error querying data");
    } else {
      if (result.rows.length > 0) {
        const user = result.rows[0];
        bcrypt.compare(password, user.password, (err, match) => {
          if (match) {
            studentName = user.first_name + " " + user.last_name;
            clientUsername = username;
            console.log("hello");
            res.redirect("/student/dashboard");
          } else {
            res.status(401).send("Invalid credentials");
          }
        });
      } else {
        res.status(401).send("Invalid credentials");
      }
    }
  });
});
//------------------------

//STUDENT REGISTER AND POST
app.post("/student/register", async (req, res) => {
  const username = req.body.username;
  const password = req.body.password;
  const firstName = req.body.firstName;
  const lastName = req.body.lastName;

  const hashedPassword = await bcrypt.hash(password, saltRounds);
  const query = `INSERT INTO studentusers (username, password, first_name, last_name) VALUES ($1, $2, $3, $4)`;
  const values = [username, hashedPassword, firstName, lastName];

  //This part is written by AI
  db.query(query, values, (err, result) => {
    if (err) {
      console.error("Error inserting data:", err);
      res.status(500).send("Error inserting data");
    } else {
      studentName = firstName + " " + lastName;
      clientUsername = username;
      console.log("Data inserted successfully");
      res.redirect("/student/dashboard");
    }
  });
});

app.get("/student/register", (req, res) => {
  res.render("studentRegister.ejs");
});
//------------------------

//TEACHER DASHBOARD
app.get("/teacher/dashboard", async (req, res) => {
  const result = await checkIfTeacherCreatedClass();
  if (result) {
    let query = "SELECT class_code FROM teacherusers WHERE username = $1";
    let values = [clientUsername];
    let classCodeResult = await db.query(query, values);

    query = "SELECT (first_name, last_name) FROM studentusers WHERE class_code = $1";
    values = [classCodeResult.rows[0].class_code];

    let studentNameResult = await db.query(query, values);
    console.log("------------------------------------");
    console.log(studentNameResult.rows);
    console.log("---------------------------------");

    query = "SELECT assigned FROM teacherusers WHERE username = $1";
    values = [clientUsername];
    const assignmentsResult = await db.query(query, values);
    console.log(assignmentsResult);
    const intArray = assignmentsResult.rows[0].assigned.split(',').map(value => {
      return parseInt(value);
    }
    );
    console.log(intArray);
    res.render("teacherDashboard.ejs", { teacherN: teacherName, teacherClassCode: classCodeResult.rows[0].class_code, students: studentNameResult.rows, numProj: intArray[0], numEPE: intArray[1] });
  }
  else {
    //if techer hasn't generated a class code yet
    res.render("teacherDashboardCode.ejs", { teacherN: teacherName });
  }
});

//STUDENT DASHBOARD
app.get("/student/dashboard", async (req, res) => {
  const result = await checkIfStudentJoinedClass();
  console.log(result);
  if (result) {
    let query = "SELECT class_code FROM studentusers WHERE username = $1";
    let values = [clientUsername];
    let databaseResult = await db.query(query, [clientUsername]);
    let studentEnteredClassCode = databaseResult.rows[0].class_code;

    query = "SELECT name FROM teacherusers WHERE class_code = $1";
    values = [studentEnteredClassCode];
    const nameResult = await db.query(query, values);
    res.render("studentDashboard.ejs", { studentN: studentName, teacherN: nameResult.rows[0].name });
  }
  else {
    res.render("studentDashboardCode.ejs", { studentN: "studentName" });
  }
});

//LOGOUT
app.get("/logout", (req, res) => {
  res.redirect("/");
});

//GENERATE CODE FOR STUDENT CLASSES
app.get("/generateClassCode", async (req, res) => {
  let classCode = Math.floor(100000 + Math.random() * 900000);

  let query = "UPDATE teacherusers SET class_code = $1, created_class = $2 WHERE username = $3;";
  let values = [classCode, true, clientUsername];
  await db.query(query, values, (err, result) => {
    if (err) {
      console.error("Error inserting data:", err);
      res.status(500).send("Error inserting data.");
    } else {
      console.log("Data inserted successfully");
      res.redirect("/teacher/dashboard");
    }
  });
});

app.post("/studentJoinClass", async (req, res) => {
  console.log("THIS IS RUNNING")

  const studentClassCode = req.body.classCode;
  const query = "UPDATE studentusers SET class_code = $1, joined_class = $2 WHERE username = $3;";
  const values = [studentClassCode, true, clientUsername];

  await db.query(query, values, (err, result) => {
    if (err) {
      console.error("Error inserting data:", err);
      res.status(500).send("Error inserting data.");
    } else {
      console.log("Data inserted successfully");
      res.redirect("/student/dashboard");
    }
  });
});

async function checkIfTeacherCreatedClass() {
  const query = "SELECT created_class FROM teacherusers WHERE username = $1";
  const values = [clientUsername];

  try {
    const result = await db.query(query, values);
    console.log("DID THE TEACHER CREATE A CLASS: "+ result.rows[0].created_class);
    return result.rows[0].created_class;
  } catch (err) {
    console.error("Error querying data:", err);
  }
  return false;
}

async function checkIfStudentJoinedClass() {
  const query = "SELECT joined_class FROM studentusers WHERE username = $1";
  const values = [clientUsername];

  try {
    const result = await db.query(query, values);
    console.log("DID THE STUDENT JOIN A CLASS: "+ result.rows[0].joined_class);
    return result.rows[0].joined_class;
  } catch (err) {
    console.error("Error querying data:", err);
  }
  return false;
}

app.post("/teacher/updateAssignments" , async (req, res) => {
  // const assignment = req.body.assignment;
  // const query = "UPDATE teacherusers SET assignments = $1 WHERE username = $2";
  // const values = [assignment, clientUsername];

  const projQuestions = req.body.projAssign;
  const epeQuestions = req.body.epeAssign;
  console.log("body", req.body);
  const query = "UPDATE teacherusers SET assigned = $1 WHERE username = $2";
  const values = [projQuestions+","+epeQuestions, clientUsername];

  db.query(query, values, (err, result) => {
    if (err) {
      console.error("Error inserting data:", err);
      res.status(500).send("Error inserting data.");
    } else {
      console.log("Data inserted successfully");
    }
  })

  res.redirect("/teacher/dashboard");

  // await db.query(query, values, (err, result) => {
  //   if (err) {
  //     console.error("Error inserting data:", err);
  //     res.status(500).send("Error inserting data.");
  //   } else {
  //     console.log("Data inserted successfully");
  //     res.redirect("/teacher/dashboard");
  //   }
  // });
});

app.post('/unityStudentLogin', (req, res) => {
  const username = req.body.username;
  const password = req.body.password;

  const query = `SELECT * FROM studentusers WHERE username = $1`;
  const values = [username];

  console.log(username, password);

  db.query(`SELECT * FROM studentusers WHERE username = $1`, [username], async (err, result) => {
    if (err) {
      console.error("Error querying data:", err);
      res.status(500).send("Error querying data");
    } else {
      if (result.rows.length > 0) {
        const user = result.rows[0];

        bcrypt.compare(password, user.password, async (err, match) => {
          if (match) {
            const newResult = await db.query(`SELECT assigned FROM teacherusers WHERE class_code = $1`, [user.class_code]);
            res.json({ success: true, username: username, name: result.rows[0].first_name + " " + result.rows[0].last_name, assigned: newResult.rows[0].assigned});
          } else {
            res.status(401).send("Invalid credentials");
          }
        });
      } else {
        res.json({ success: false, message: "Invalid credentials" });
      }
    }
  });
});

app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});