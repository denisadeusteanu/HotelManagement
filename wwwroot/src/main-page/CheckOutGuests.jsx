import React, { useState, useEffect } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { List, ListItem, ListItemText, Switch, ListItemSecondaryAction, ListSubheader, Typography } from '@material-ui/core';
import axios from 'axios'
import moment from 'moment'

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    maxWidth: 420,
    backgroundColor: theme.palette.background.paper,
  },
  title: {
    margin: theme.spacing(2, 0, 1),
    textAlign: "center"
  }
}));

export default function CheckInGuests() {
  const classes = useStyles();
  const [reservations, setReservations] = useState([]);
  const [checked, setChecked] = useState([]);

  useEffect(() => {
    axios.get('/api/reservations')
      .then(response => {
        setReservations(response.data
          .filter(res => moment(res.checkOutDate).format("YYYY-MM-DD") ===  moment(new Date()).format("YYYY-MM-DD")));
        setChecked(response.data.filter(res => res.reservationState === 2).map(res => res.id))
      });

  }, []);

  const handleToggle = (reservationId) => () => {
    const currentIndex = checked.indexOf(reservationId);
      const newChecked = [...checked];
    
      if (currentIndex === -1) {
        newChecked.push(reservationId);
        updateReservation(reservationId, 2, 0);
      } else {
        newChecked.splice(currentIndex, 1);
        updateReservation(reservationId, 0, 1)
      }
    
      setChecked(newChecked);
  };

  const updateReservation = (reservationId, reservationState, isOccupied) => {
    const [reservation] = reservations
        .filter(reservation => reservation.id == reservationId)
      reservation.reservationState = reservationState;
      reservation.room.isOccupied = isOccupied;
      axios.put('api/reservations', reservation)
      .then(response => {
        console.log(response);
      }, error => {
        console.log(error.response);
      });
  }

  return (
    <>
      {reservations.length > 0 
      ?<Typography variant="h6" className={classes.title}>
      Lista Checkout Astazi
       </Typography>
      : null}
      <List className={classes.root}>
      {
          reservations.map((reservation) => {
          return (
              <ListItem dense key={reservation.id}>
              <ListItemText primary={`Camera:${reservation.room.roomNumber} - ${reservation.guest.firstName} ${reservation.guest.lastName} - ${reservation.guest.phoneNumber}`} />
              <ListItemSecondaryAction>
              <Switch
                  edge="end"
                  onChange={handleToggle(reservation.id)}
                  checked={checked.indexOf(reservation.id) !== -1}
                  color="primary"
                  // inputProps={{ 'aria-labelledby': 'switch-list-label-wifi' }}
              />
              </ListItemSecondaryAction>
              </ListItem>
          );
          })
      }
      </List>
    </>
  );
}