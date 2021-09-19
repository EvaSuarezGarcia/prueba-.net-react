import React from "react";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import CardList from "./CardList/CardList";
import CardFormDialog from "./FormDialog/CardFormDialog";
import { CardListProps } from "./CardList/CardList";
import { InfoCardData } from "./CardList/InfoCard/InfoCard";
import AddFab from "./Buttons/AddFab";
import * as Constants from "../Constants";

const theme = createTheme({
    palette: {
        background: {
            default: "#eeeeee",
        },
    },
});

const App: React.FC = () => {
    // --- Cards list ---
    const [cards, setCards] = React.useState<CardListProps["cards"]>([
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

    // --- Add card dialog ---
    const [showAddDialog, setShowAddDialog] = React.useState(false);

    const handleClickOpenAddDialog = () => {
        setShowAddDialog(true);
    };

    const handleCloseAddDialog = () => {
        setShowAddDialog(false);
    };

    const addCard = (card: InfoCardData) => {
        setCards([...cards, card]);
    };

    // --- Edit card dialog callback ---
    const editCard = (card: InfoCardData) => {
        const newCards = cards.map((thisCard) => {
            if (thisCard.key === card.key) {
                return card;
            } else {
                return thisCard;
            }
        });
        setCards(newCards);
    };

    return (
        <>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <CardList cards={cards} editCard={editCard} />
                <AddFab onClick={handleClickOpenAddDialog} />
                <CardFormDialog
                    open={showAddDialog}
                    callback={addCard}
                    handleClose={handleCloseAddDialog}
                    dialogTitle={Constants.NEW_CARD}
                    dialogButton={Constants.ADD}
                    initialCardData={{
                        title: "",
                        description: "",
                        image: "",
                        key: 0,
                    }}
                />
            </ThemeProvider>
        </>
    );
};

export default App;
