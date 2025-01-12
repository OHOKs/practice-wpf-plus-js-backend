const express = require('express');
const { createUser, getAllUsers, getUserById, deleteUser } = require('../controllers/user.controller');
const router = express.Router();

router.post('/users', createUser);
router.get('/users', getAllUsers); 
router.get('/users/:id', getUserById); 
router.delete('/users/:id', deleteUser);

module.exports = router;