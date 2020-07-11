import React from 'react';
import { render } from 'react-dom';
import CheckInGuests from './main-page/CheckInGuests';
import CheckOutGuests from './main-page/CheckOutGuests';
import OccupationChart from './main-page/OccupationChart'



render(<CheckInGuests />, document.getElementById("main-page"));
render(<CheckOutGuests />, document.getElementById("main-page1"));
render(<OccupationChart />, document.getElementById("occupation-chart"));