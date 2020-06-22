import React, { Component } from 'react';
import ReactDOM, { render } from 'react-dom';
import Button from '@material-ui/core/Button';
import { withStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';

const useStyles = (theme) => ({
  container: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  textField: {
    marginLeft: theme.spacing(1),
    marginRight: theme.spacing(1),
    width: 200,
  },
});

export default class FormDialog extends Component {
  state = {
    open: false
  }

  componentDidMount() {
    this.handleClickOpen();
  }

  handleClickOpen = () => {
    this.setState({
      open: true
    })
  };

  handleClose = () => {
    this.setState({
      open: false
    })
    ReactDOM.unmountComponentAtNode(document.getElementById('modal'));
  };

  render() {
    const classes = this.styles();
    return (
      <div>
        <Dialog open={this.state.open} onClose={this.handleClose} aria-labelledby="form-dialog-title">
          <DialogTitle id="form-dialog-title">Add Reservation</DialogTitle>
          <DialogContent>
            <DialogContentText>
              To subscribe to this website, please enter your email address here. We will send updates
              occasionally.
          </DialogContentText>
            <TextField
              autoFocus
              margin="dense"
              id="name"
              label="Email Address"
              type="email"
              fullWidth
            />
            <TextField
              id="date"
              label="Check-in"
              type="date"
              defaultValue={this.props.startDate}
              className={classes.textField}
              InputLabelProps={{
                shrink: true,
              }}
            />
            <TextField
              id="date"
              label="Check-out"
              type="date"
              defaultValue={this.props.endDate}
              className={classes.textField}
              InputLabelProps={{
                shrink: true,
              }}
            />
          </DialogContent>
          <DialogActions>
            <Button onClick={this.handleClose} color="primary">
              Cancel
          </Button>
            <Button onClick={this.handleClose} color="primary">
              Subscribe
          </Button>
          </DialogActions>
        </Dialog>
      </div>
    );
  }
}