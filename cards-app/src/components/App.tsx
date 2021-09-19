import React from "react";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import CardList from "./CardList/CardList";
import CardFormDialog from "./FormDialog/CardFormDialog";
import { Props as CardListProps } from "./CardList/CardList";
import { Props as InfoCardProps } from "./CardList/InfoCard/InfoCard";
import AddFab from "./Buttons/AddFab";

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
            key: 1,
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://es.himgs.com/imagenes/estar-bien/20210217184541/gatos-gestos-lenguaje-significado/0-922-380/gatos-gestos-m.jpg",
            key: 2,
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://estaticos.muyinteresante.es/media/cache/1140x_thumb/uploads/images/gallery/59c4f5655bafe82c692a7052/gato-marron_0.jpg",
            key: 3,
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://estaticos.muyinteresante.es/media/cache/1140x_thumb/uploads/images/gallery/59c4f5655bafe82c692a7052/gato-marron_0.jpg",
            key: 4,
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://estaticos.muyinteresante.es/media/cache/1140x_thumb/uploads/images/gallery/59c4f5655bafe82c692a7052/gato-marron_0.jpg",
            key: 5,
        },
        {
            title: "Gato",
            description: "Gato súper bonito cuqui kawaii desu ne",
            image: "https://estaticos.muyinteresante.es/media/cache/1140x_thumb/uploads/images/gallery/59c4f5655bafe82c692a7052/gato-marron_0.jpg",
            key: 6,
        },
    ]);

    const addCard = (card: InfoCardProps) => {
        setCards([...cards, card]);
    };

    return (
        <>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <CardList cards={cards} />
                <CardFormDialog
                    callbackCard={addCard}
                    dialogOpenButton={<AddFab />}
                />
            </ThemeProvider>
        </>
    );
};

export default App;
