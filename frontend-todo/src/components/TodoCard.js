import React from 'react';

const TodoCard = ({ item, onDelete }) => {
  return (
    <div style={{
      background: '#e0e5ec',
      borderRadius: '20px',
      boxShadow: '9px 9px 16px rgb(163,177,198,0.6), -9px -9px 16px rgba(255,255,255, 0.5)',
      padding: '20px',
      margin: '10px',
      color: '#005f99' // Ese azul elegante que buscamos
    }}>
      <h3>{item.name}</h3>
      <button onClick={() => onDelete(item.id)}>Borrar</button>
    </div>
  );
};

export default TodoCard;