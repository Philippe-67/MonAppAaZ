// import React, { useEffect, useState } from 'react';

// const PrenomsPage: React.FC = () => {
// 	const [prenoms, setPrenoms] = useState<{ nom: string }[]>([]);
// 	const apiUrl = import.meta.env.VITE_API_URL;

// 	useEffect(() => {
// 		if (!apiUrl) return;
// 		fetch(`${apiUrl}/api/prenoms`)
// 			.then(res => {
// 				if (!res.ok) throw new Error(`Erreur ${res.status}`);
// 				return res.json();
// 			})
// 			.then(data => setPrenoms(data))
// 			.catch(err => console.error('Erreur lors du chargement des prénoms :', err));
// 	}, [apiUrl]);

// 	return (
// 		<div style={{ padding: '20px' }}>
// 			<h1>Liste des Prénoms</h1>
// 			<ul>
// 				{prenoms.length > 0 ? (
// 					prenoms.map((prenom, index) => (
// 						<li key={index}>{prenom.nom}</li>
// 					))
// 				) : (
// 					<p>Aucun prénom à afficher</p>
// 				)}
// 			</ul>
// 		</div>
// 	);
// };

// export default PrenomsPage;
