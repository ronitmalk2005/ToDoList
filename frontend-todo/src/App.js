import axios from 'axios';
import { useEffect, useState } from 'react';
import TodoCard from './components/TodoCard'; // Importamos tu tarjeta

function App() {
  const [items, setItems] = useState([]);

  useEffect(() => {
    // Aquí llamamos a tu API de .NET
    axios.get('http://localhost:5072/api/todo')
      .then(res => setItems(res.data))
      .catch(err => console.error("Error al conectar:", err));
  }, []);

  return (
    <div className="App">
      <h1>Mis Tareas</h1>
      {/* Esto recorre la lista y crea una tarjeta para cada item */}
      {items.map(item => (
        <TodoCard key={item.id} item={item} />
      ))}
    </div>
  );
}

export default App;