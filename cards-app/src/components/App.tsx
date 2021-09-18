import React from "react";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import CardList from "./CardList/CardList";
import CardFormDialog from "./FormDialog/CardFormDialog";
import { Props as CardListProps } from "./CardList/CardList";

const theme = createTheme({
    palette: {
        background: {
            default: "#eeeeee",
        },
    },
});

export interface State {
    cards: CardListProps["cards"];
}

const App: React.FC = () => {
    const [cards, setCards] = React.useState<State["cards"]>([
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
    ]);

    return (
        <>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <CardList cards={cards} />
                <CardFormDialog cards={cards} setCards={setCards} />
            </ThemeProvider>
        </>
    );
};

export default App;
