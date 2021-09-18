import React from "react";
import { CssBaseline } from "@mui/material";
import CardList from "./components/CardList/CardList";
import { Props as CardListProps } from "./components/CardList/CardList";

const App: React.FC = () => {
    const cards: CardListProps["cards"] = [
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://estaticos.muyinteresante.es/media/cache/1140x_thumb/uploads/images/gallery/59c4f5655bafe82c692a7052/gato-marron_0.jpg",
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://es.himgs.com/imagenes/estar-bien/20210217184541/gatos-gestos-lenguaje-significado/0-922-380/gatos-gestos-m.jpg",
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://estaticos.muyinteresante.es/media/cache/1140x_thumb/uploads/images/gallery/59c4f5655bafe82c692a7052/gato-marron_0.jpg",
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://estaticos.muyinteresante.es/media/cache/1140x_thumb/uploads/images/gallery/59c4f5655bafe82c692a7052/gato-marron_0.jpg",
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://estaticos.muyinteresante.es/media/cache/1140x_thumb/uploads/images/gallery/59c4f5655bafe82c692a7052/gato-marron_0.jpg",
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://estaticos.muyinteresante.es/media/cache/1140x_thumb/uploads/images/gallery/59c4f5655bafe82c692a7052/gato-marron_0.jpg",
        },
    ];

    return (
        <React.Fragment>
            <CssBaseline />
            <CardList cards={cards} />
        </React.Fragment>
    );
};

export default App;
