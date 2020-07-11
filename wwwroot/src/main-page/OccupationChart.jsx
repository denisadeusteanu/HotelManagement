import React, {useState, useEffect} from 'react'
import { PieChart } from 'react-minimal-pie-chart';
import axios from 'axios'

 export default function OccupationChart() {
  const [rooms, setRooms] = useState([]);

  useEffect(() => {
    axios.get('/api/rooms')
    .then(response => {
      setRooms(response.data)
    })
  }, []);

  const getOccupiedRooms = () => {
    return rooms.filter(room => room.isOccupied == 1).length
  }

  const getVacantRooms = () => {
    return rooms.filter(room => room.isOccupied == 0).length
  }

   return (
    <PieChart
      data={[
        { title: 'Ocupat', value: getOccupiedRooms(), color: '#48c52c' },
        { title: 'Neocupat', value: getVacantRooms(), color: '#fd0f2b' },
      ]}
      label={({ dataEntry}) => Math.round(dataEntry.percentage) + '% ' + dataEntry.title}
      labelPosition={112}
      radius={42}
      style={{ 
        height: '400px',
        fontSize: '5px'
      }}
    />);
}