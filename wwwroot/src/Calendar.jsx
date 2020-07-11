import React, { Component } from 'react';
import { render } from 'react-dom';
import FullCalendar from '@fullcalendar/react'
import resourceTimelinePlugin from '@fullcalendar/resource-timeline'
import interactionPlugin from '@fullcalendar/interaction'
import axios from 'axios'
import moment from 'moment'
import FormDialog from './FormDialog'

import './main.scss'

export default class Calendar extends Component {
  state = {
    rooms: [],
    reservations: [],
    reservation: null,
    open: true
  }

  componentDidMount() {
    axios.get('/api/rooms')
      .then(response => {
        console.log(response);
        this.setState({
          rooms: response.data.filter(room => room.isUsable === 1)
        })
      })

    axios.get('/api/reservations')
      .then(response => {
        console.log(response);
        let bookings = response.data.map(function (reservation) {
          return {
            id: reservation.id,
            title: `${reservation.guest.firstName} ${reservation.guest.lastName} ${reservation.guest.phoneNumber}`,
            start: moment(reservation.checkinDate).format("YYYY-MM-DD"),
            end: moment(reservation.checkOutDate).format("YYYY-MM-DD"),
            resourceId: reservation.room.id
          }
        });
        console.log(bookings);
        this.setState({
          reservations: bookings
        })
      })
  }

  handleEventClick = (info) => {
    axios.get(`/api/reservations/${info.event.id}`)
      .then(response => {
        console.log(response);
        const reservation = response.data;
        reservation.mode = 'edit';
        render(<FormDialog {...reservation} />, document.getElementById("modal"));
      });
  }

  handleDateSelect = (selectInfo) => {
    const reservation = {
      startDate: selectInfo.startStr,
      endDate: selectInfo.endStr,
      roomId: selectInfo.resource.id
    }

    render(<FormDialog {...reservation} />, document.getElementById("modal"));
    return selectInfo;
  }

  eventRender(info) {
    var tooltip = new Tooltip(info.el, {
      title: info.el.text,
      placement: 'bottom',
      trigger: 'hover',
      container: 'body'
    });
  }

  render() {
    const { rooms, reservations } = this.state;
    return (
      <div>
        <FullCalendar
          schedulerLicenseKey="GPL-My-Project-Is-Open-Source"
          plugins={[resourceTimelinePlugin, interactionPlugin]}
          selectable={true}
          editable={true}
          header={{
            left: 'today prev,next',
            center: 'title',
            right: 'resourceTimelineLuna'
          }}
          defaultView={'resourceTimelineMonth'}
          aspectRatio={1.5}
          views={
            {
              resourceTimelineLuna: {
                type: 'resourceTimeline',
                duration: { months: 1 },
                buttonText: 'luna'
              }
            }
          }
          resourceAreaWidth={'8%'}
          resourceColumns={[{
            labelText: 'Camere',
            field: 'roomNumber'
          }]}
          resources={rooms}
          events={reservations}
          eventClick={this.handleEventClick}
          select={this.handleDateSelect}
          eventRender={this.eventRender}
        />
      </div>
    );
  }
}