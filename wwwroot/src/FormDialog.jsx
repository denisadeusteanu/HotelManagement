import React, { useState, useEffect, useRef } from 'react';
import ReactDOM, { render } from 'react-dom';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import Alert from '@material-ui/lab/Alert'
import axios from 'axios';
import moment from 'moment'

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
  const [firstName, setFirstName] = useState(props.guest?.firstName ? props.guest.firstName : "");
  const [lastName, setLastName] = useState(props.guest?.lastName ?? "");
  const [phoneNumber, setPhoneNumber] = useState(props.guest?.phoneNumber ?? "");
  const [email, setEmail] = useState(props.guest?.email ?? "");
  const [checkinDate, setCheckinDate] = useState(props.checkinDate ?
    moment(props.checkinDate).format("YYYY-MM-DD") : props.startDate);
  const [checkoutDate, setCheckoutDate] = useState(props.checkOutDate ?
    moment(props.checkOutDate).format("YYYY-MM-DD") : props.endDate);
  const [error, setError] = useState("");
  const [isSubmitDisabled, setIsSubmitDisabled] = useState(true);

  const didMountRef = useRef(false);
  const classes = useStyles();

  useEffect(() => {
    if(didMountRef.current){
      setIsSubmitDisabled(firstName === "" || phoneNumber === "" || checkinDate === "" || checkoutDate === "");
    }
    else {
      handleClickOpen();
      didMountRef.current = true
    }
  });

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    ReactDOM.unmountComponentAtNode(document.getElementById('modal'));
  };

  const handleSubmit = () => {
    if (props.mode === 'edit') {
      let reservation = {
        id: props.id,
        guest: {
          id: props.guest?.id,
          firstName,
          lastName,
          phoneNumber,
          email
        },
        checkinDate,
        checkoutDate,
        room: props.room
      };
      axios.put('api/reservations', reservation)
        .then(response => {
          handleClose();
          location.reload();
        }, error => {
          console.log(error.response);
          setError(error.response.data)
        });
    }
    else {
      let reservation = {
        id: props.id,
        guest: {
          id: props.guest?.id,
          firstName,
          lastName,
          phoneNumber,
          email
        },
        checkinDate,
        checkoutDate,
        room: {
          id: props.roomId
        }
      };

      axios.post('api/reservations', reservation)
        .then(response => {
          console.log(response);
          handleClose();
          location.reload();
        }, error => {
          console.log(error.response);
          setError(error.response.data)
        });
    }
  }

  const handleDelete = () => {
    axios.delete(`api/reservations/${props.id}`)
      .then(response => {
        handleClose();
        location.reload();
      }, error => {
        console.log(error);
        setError(error.response.data)
      });
  }

  return (
    <div>
      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        <DialogTitle id="form-dialog-title">{props.mode === 'edit' ? 'Editeaza/Sterge rezervarea' : 'Adauga rezervarea'}</DialogTitle>
        <DialogContent>
          <div>
            <TextField
              margin="none"
              id="firstName"
              label="Prenume"
              className={classes.textField}
              value={firstName}
              onChange={e => setFirstName(e.target.value)}
              error={firstName === ""}
              helperText={firstName === "" ? "Prenumele nu poate fi gol" : ''}
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
              label="Numar de telefon"
              type="number"
              className={classes.textField}
              value={phoneNumber}
              onChange={e => setPhoneNumber(e.target.value)}
              error={phoneNumber === ""}
              helperText={phoneNumber === "" ? "Numarul de telefon nu poate fi gol" : ''}
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
              format="dd/MM/yyyy"
              onChange={e => {setCheckinDate(e.target.value); setError("")}}
              className={classes.textField}
              InputLabelProps={{
                shrink: true,
              }}
              error={checkinDate === ""}
              helperText={checkinDate === "" ? "Data de check-in nu poate fi gola" : ''}
            />
          </div>
          <div>
            <TextField
              id="date"
              margin="none"
              label="Check-out"
              type="date"
              value={checkoutDate}
              format="DD/MM/yyyy"
              onChange={e => {setCheckoutDate(e.target.value); setError("")}}
              className={classes.textField}
              InputLabelProps={{
                shrink: true,
              }}
              error={checkoutDate === ""}
              helperText={checkinDate === "" ? "Data de check-out nu poate fi gola" : ''}
            />
          </div>
        </DialogContent>
        { error ? <Alert severity="error">{error}</Alert> : <div></div>} 
        <DialogActions>
          {props.mode === 'edit' ?
            <Button onClick={handleDelete} color="primary">
              Sterge
          </Button> : <div></div>}
          <Button onClick={handleClose} color="primary">
            Renunta
          </Button>
          <Button onClick={handleSubmit} color="primary" disabled={isSubmitDisabled}>
            Salveaza
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}