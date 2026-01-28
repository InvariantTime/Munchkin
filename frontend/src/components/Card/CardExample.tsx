import React, { useState, useMemo, useCallback } from 'react';

// --- Типы ---
type Rarity = 'common' | 'rare' | 'epic' | 'legendary';

interface CardData {
  id: number;
  title: string;
  cost: number;
  rarity: Rarity;
  gradient: string;
  desc: string;
}

interface PositionStyle {
  left: string;
  transform: string;
  zIndex: number;
  transition: string;
}

// --- Шаблоны карт ---
const TEMPLATES: Omit<CardData, 'id'>[] = [
  { title: "Огненный шар", cost: 2, rarity: "common", gradient: "linear-gradient(45deg, #e74c3c, #c0392b)", desc: "Наносит 5 урона" },
  { title: "Ледяная стрела", cost: 3, rarity: "rare", gradient: "linear-gradient(45deg, #3498db, #2980b9)", desc: "Замораживает" },
  { title: "Дракон", cost: 8, rarity: "legendary", gradient: "linear-gradient(45deg, #f39c12, #e67e22)", desc: "8/8 полет" },
  { title: "Зелье", cost: 1, rarity: "common", gradient: "linear-gradient(45deg, #27ae60, #229954)", desc: "Лечит 5 HP" },
  { title: "Молния", cost: 3, rarity: "rare", gradient: "linear-gradient(45deg, #9b59b6, #8e44ad)", desc: "6 урона всем" },
  { title: "Щит", cost: 2, rarity: "common", gradient: "linear-gradient(45deg, #7f8c8d, #616a6b)", desc: "Блок 8" },
  { title: "Вампир", cost: 4, rarity: "epic", gradient: "linear-gradient(45deg, #c0392b, #8e44ad)", desc: "Кража 4" }
];

// --- Математика позиционирования (чистая функция) ---
const calculateFanStyle = (
  index: number, 
  totalCount: number, 
  isHovered: boolean, 
  isLeaving: boolean
): PositionStyle => {
  // Если карта уходит (сыграна/удалена), анимируем вылет вверх
  if (isLeaving) {
    return {
      left: '50%',
      transform: 'translateX(-50%) translateY(-400px) scale(0.5)',
      zIndex: 100,
      transition: 'all 0.4s cubic-bezier(0.25, 0.8, 0.25, 1)'
    };
  }

  const maxAngle = 35; // Максимальный разворот веера (градусы)
  const maxSpacing = 140; // Максимальное расстояние между картами
  
  // Если только одна карта - центрируем без поворота
  if (totalCount === 1) {
    return {
      left: '50%',
      transform: isHovered 
        ? 'translateX(-50%) translateY(-100px) scale(1.15)' 
        : 'translateX(-50%) rotate(0deg)',
      zIndex: isHovered ? 100 : 10,
      transition: 'all 0.3s cubic-bezier(0.25, 0.8, 0.25, 1)'
    };
  }

  // Интерполяция угла: от -35 до +35 градусов равномерно
  const angleStep = (maxAngle * 2) / (totalCount - 1);
  const angle = -maxAngle + (angleStep * index);
  
  // Адаптивное смещение: при >5 картах они плотнее
  const spacing = Math.min(maxSpacing, 700 / totalCount);
  
  // Центрирование: (index - center) дает отрицательные значения слева, положительные справа
  const centerOffset = (totalCount - 1) / 2;
  const offset = (index - centerOffset) * spacing;
  
  // Формируем transform
  const baseTransform = `translateX(calc(-50% + ${offset}px)) rotate(${angle}deg)`;
  const hoverTransform = `translateX(calc(-50% + ${offset}px)) translateY(-100px) rotate(0deg) scale(1.15)`;
  
  return {
    left: '50%',
    transform: isHovered ? hoverTransform : baseTransform,
    zIndex: isHovered ? 100 : index + 1, // При наведении всплываем выше всех
    transition: 'all 0.3s cubic-bezier(0.25, 0.8, 0.25, 1)'
  };
};

// --- Компонент ---
const CardHandExm: React.FC = () => {
  const [cards, setCards] = useState<CardData[]>([]);
  const [hoveredId, setHoveredId] = useState<number | null>(null);
  const [exitingIds, setExitingIds] = useState<Set<number>>(new Set());
  const [nextId, setNextId] = useState(1);

  // Добавление карты
  const addCard = useCallback(() => {
    const template = TEMPLATES[Math.floor(Math.random() * TEMPLATES.length)];
    const newCard: CardData = { ...template, id: nextId };
    setCards(prev => [...prev, newCard]);
    setNextId(prev => prev + 1);
  }, [nextId]);

  // Удаление с анимацией
  const removeCard = useCallback((id: number) => {
    setExitingIds(prev => new Set(prev).add(id));
    setTimeout(() => {
      setCards(prev => prev.filter(c => c.id !== id));
      setExitingIds(prev => {
        const next = new Set(prev);
        next.delete(id);
        return next;
      });
    }, 400); // Ждем окончания CSS-перехода
  }, []);

  // "Игра" карты (клик)
  const playCard = useCallback((id: number) => {
    removeCard(id);
  }, [removeCard]);

  // Сброс руки
  const clearHand = useCallback(() => {
    setExitingIds(new Set(cards.map(c => c.id)));
    setTimeout(() => {
      setCards([]);
      setExitingIds(new Set());
    }, 400);
  }, [cards]);

  // Генерируем стили для каждой карты мемоизированно
  const cardStyles = useMemo(() => {
    return cards.map((card, index) => 
      calculateFanStyle(
        index, 
        cards.length, 
        hoveredId === card.id, 
        exitingIds.has(card.id)
      )
    );
  }, [cards, hoveredId, exitingIds]);

  return (
    <div style={styles.container}>
      {/* Панель управления */}
      <div style={styles.controls}>
        <button onClick={addCard} style={{...styles.button, ...styles.addBtn}}>
          + Добавить карту
        </button>
        <button 
          onClick={() => cards.length > 0 && removeCard(cards[cards.length - 1].id)} 
          disabled={cards.length === 0}
          style={{...styles.button, ...styles.removeBtn, opacity: cards.length ? 1 : 0.5}}
        >
          - Удалить последнюю
        </button>
        <button 
          onClick={clearHand} 
          disabled={cards.length === 0}
          style={{...styles.button, ...styles.clearBtn, opacity: cards.length ? 1 : 0.5}}
        >
          × Сбросить всё
        </button>
        <div style={styles.counter}>
          Карт: <strong>{cards.length}</strong>
        </div>
      </div>

      {/* Рука */}
      <div style={styles.handContainer}>
        {cards.length === 0 && (
          <div style={styles.emptyState}>Рука пуста...</div>
        )}
        
        {cards.map((card, index) => {
          const style = cardStyles[index];
          const isLeaving = exitingIds.has(card.id);
          
          return (
            <div
              key={card.id}
              onMouseEnter={() => !isLeaving && setHoveredId(card.id)}
              onMouseLeave={() => setHoveredId(null)}
              onClick={() => playCard(card.id)}
              style={{
                ...styles.card,
                ...styles[`rarity${card.rarity}` as const],
                ...style,
                cursor: isLeaving ? 'default' : 'pointer',
                pointerEvents: isLeaving ? 'none' : 'auto'
              }}
            >
              <div style={styles.cardHeader}>
                <div style={styles.cardCost}>{card.cost}</div>
                <div style={styles.cardTitle}>{card.title}</div>
              </div>
              <div style={{...styles.cardImage, background: card.gradient}} />
              <div style={styles.cardBody}>{card.desc}</div>
            </div>
          );
        })}
      </div>

      {/* Подсказка */}
      <div style={styles.hint}>
        Наведите для поднятия • Клик для игры
      </div>
    </div>
  );
};

// --- Стили (CSS-in-JS объект для компактности) ---
const styles: Record<string, React.CSSProperties> = {
  container: {
    minHeight: '100vh',
    background: 'linear-gradient(135deg, #1a1a2e 0%, #16213e 50%, #0f3460 100%)',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    fontFamily: "'Cinzel', serif",
    overflow: 'hidden',
    padding: '20px'
  },
  controls: {
    position: 'fixed',
    top: 30,
    display: 'flex',
    gap: 15,
    zIndex: 1000,
    background: 'rgba(0,0,0,0.4)',
    padding: '15px 25px',
    borderRadius: 50,
    border: '1px solid rgba(255,255,255,0.1)',
    backdropFilter: 'blur(10px)'
  },
  button: {
    padding: '12px 24px',
    border: 'none',
    borderRadius: 25,
    cursor: 'pointer',
    fontFamily: 'inherit',
    fontWeight: 'bold',
    textTransform: 'uppercase',
    letterSpacing: 1,
    transition: 'all 0.3s',
    color: 'white',
    fontSize: 14
  },
  addBtn: {
    background: 'linear-gradient(145deg, #27ae60, #229954)',
    boxShadow: '0 4px 15px rgba(39, 174, 96, 0.4)'
  },
  removeBtn: {
    background: 'linear-gradient(145deg, #e74c3c, #c0392b)',
    boxShadow: '0 4px 15px rgba(231, 76, 60, 0.4)'
  },
  clearBtn: {
    background: 'linear-gradient(145deg, #7f8c8d, #616a6b)',
    boxShadow: '0 4px 15px rgba(127, 140, 141, 0.4)'
  },
  counter: {
    color: 'white',
    display: 'flex',
    alignItems: 'center',
    paddingLeft: 15,
    borderLeft: '1px solid rgba(255,255,255,0.2)',
    fontSize: 16
  },
  handContainer: {
    position: 'relative',
    width: '90vw',
    maxWidth: 1000,
    height: 400,
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'flex-end',
    perspective: '1000px',
    marginBottom: 50
  },
  emptyState: {
    position: 'absolute',
    color: 'rgba(255,255,255,0.3)',
    fontSize: 18,
    bottom: 100,
    pointerEvents: 'none'
  },
  card: {
    position: 'absolute',
    width: 140,
    height: 210,
    background: 'linear-gradient(145deg, #2a2a3e 0%, #1a1a2e 100%)',
    border: '3px solid #4a4a6a',
    borderRadius: 12,
    transformOrigin: 'bottom center',
    boxShadow: '0 8px 25px rgba(0,0,0,0.5)',
    userSelect: 'none',
    bottom: 20,
    overflow: 'hidden',
    // Важно: начальная анимация появления
    animation: 'enter 0.4s ease-out forwards'
  },
  // Редкости (бордеры)
  raritycommon: { borderColor: '#95a5a6' },
  rarityrare: { borderColor: '#3498db', boxShadow: '0 0 10px rgba(52, 152, 219, 0.3)' },
  rarityepic: { borderColor: '#9b59b6', boxShadow: '0 0 15px rgba(155, 89, 182, 0.4)' },
  raritylegendary: { borderColor: '#f39c12', boxShadow: '0 0 20px rgba(243, 156, 18, 0.5)' },
  
  cardHeader: {
    height: 32,
    background: 'linear-gradient(90deg, #2c3e50, #34495e)',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-between',
    padding: '0 8px',
    borderBottom: '2px solid #4a4a6a'
  },
  cardCost: {
    width: 22,
    height: 22,
    background: 'linear-gradient(145deg, #3498db, #2980b9)',
    borderRadius: '50%',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    color: 'white',
    fontWeight: 'bold',
    fontSize: 11,
    border: '2px solid #5dade2'
  },
  cardTitle: {
    color: '#ecf0f1',
    fontSize: 10,
    fontWeight: 'bold',
    textTransform: 'uppercase'
  },
  cardImage: {
    height: 90,
    backgroundSize: 'cover',
    borderBottom: '2px solid #4a4a6a'
  },
  cardBody: {
    padding: 8,
    color: '#bdc3c7',
    fontSize: 10,
    textAlign: 'center',
    fontFamily: 'sans-serif'
  },
  hint: {
    color: 'rgba(255,255,255,0.4)',
    fontSize: 14,
    marginTop: 20
  }
};

// Добавляем keyframes для входа
const styleSheet = document.createElement("style");
styleSheet.innerText = `
  @keyframes enter {
    from { opacity: 0; transform: translateY(50px) scale(0.8); }
    to { opacity: 1; }
  }
`;
document.head.appendChild(styleSheet);

export default CardHandExm;