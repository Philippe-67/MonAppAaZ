import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import PageGestionMots from './pages/PageGestionMots';
import EditMotPage from './pages/EditMotPage';
//import PrenomsPage from './pages/PrenomsPages';
//import PrenomsPage from './pages/PrenomsPages'; // Vérifie le nom du fichier

const App: React.FC = () => (
  <Router>
    <nav style={{ padding: '10px', backgroundColor: '#f0f0f0' }}>
      <Link to="/prenoms" style={{ marginRight: '15px' }}>Prénoms</Link>
      <Link to="/mots">Mots</Link>
    </nav>
    <Routes>
     
      <Route path="/mots" element={<PageGestionMots />} />
      <Route path="/mots/:id" element={<EditMotPage />} />
      {/* <Route path="/prenoms" element={<PrenomsPage />} /> */}
      <Route path="/" element={<h2>Accueil</h2>} />
    </Routes>
  </Router>
);

export default App;
