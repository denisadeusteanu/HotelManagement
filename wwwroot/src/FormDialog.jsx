import React, { Component, useState, useEffect } from 'react';
import ReactDOM, { render } from 'react-dom';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import axios from 'axios';

const useStyles = makeStyles((theme) => ({
  container: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  textField: {
    marginLeft: theme.spacing(1),
    marginRight: theme.spacing(1),
    marginBottom: theme.spacing(1),
    width: 200,
  },
}));

export default function FormDialog(props) {
  const [open, setOpen] = useState(false);
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [email, setEmail] = useState("");
  const [checkinDate, setCheckinDate] = useState(props.startDate);
  const [checkoutDate, setCheckoutDate] = useState(props.endDate);
  const classes = useStyles();

  useEffect(() => {
    handleClickOpen();
  }, []);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    ReactDOM.unmountComponentAtNode(document.getElementById('modal'));
  };

  const handleSubmit = () => {
    const reservation = {
      guest: {
        firstName,
        lastName,
        phoneNumber,
        email
      },
      checkinDate,
      checkoutDate,
      roomId: props.roomId
    }
    axios.post('api/reservations', reservation)
      .then(response => {
        console.log(response);
        handleClose();
        location.reload();
      }, error => {
        console.log(error);
      });
  }

  return (
    <div>
      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        <DialogTitle id="form-dialog-title">Adauga rezervare</DialogTitle>
        <DialogContent>
          <div>
            <TextField
              margin="none"
              id="firstName"
              label="Prenume"
              className={classes.textField}
              value={firstName}
              onChange={e => setFirstName(e.target.value)}
            />
          </div>
          <div>
            <TextField
              margin="none"
              id="lastName"
              label="Nume"
              className={classes.textField}
              value={lastName}
              onChange={e => setLastName(e.target.value)}
            />
          </div>
          <div>
            <TextField
              margin="none"
              id="phoneNumber"
              label="Nr de telefon"
              type="number"
              className={classes.textField}
              value={phoneNumber}
              onChange={e => setPhoneNumber(e.target.value)}
            />
          </div>
          <div>
            <TextField
              margin="none"
              id="email"
              label="E-mail"
              type="email"
              className={classes.textField}
              value={email}
              onChange={e => setEmail(e.target.value)}
            />
          </div>
          <div>
            <TextField
              id="date"
              margin="none"
              label="Check-in"
              type="date"
              value={checkinDate}
              onChange={e => setCheckinDate(e.target.value)}
              className={classes.textField}
              InputLabelProps={{
                shrink: true,
              }}
            />
          </div>
          <div>
            <TextField
              id="date"
              margin="none"
              label="Check-out"
              type="date"
              value={checkoutDate}
              onChange={e => setCheckoutDate(e.target.value)}
              className={classes.textField}
              InputLabelProps={{
                shrink: true,
              }}
            />
          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Renunta
          </Button>
          <Button onClick={handleSubmit} color="primary">
            Salveaza
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}