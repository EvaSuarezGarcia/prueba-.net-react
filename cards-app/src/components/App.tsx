import React from "react";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import CardList from "./CardList/CardList";
import CardFormDialog from "./Dialogs/FormDialog/CardFormDialog";
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
    const [cards, setCards] = React.useState<CardListProps["cards"]>(
        JSON.parse(localStorage.getItem("cards") || "[]")
    );

    React.useEffect(() => {
        localStorage.setItem("cards", JSON.stringify(cards));
    }, [cards]);

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

    const deleteCard = (cardKey: InfoCardData["key"]) => {
        const newCards = cards.filter((card) => card.key !== cardKey);
        setCards(newCards);
    };

    return (
        <>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <CardList
                    cards={cards}
                    editCard={editCard}
                    deleteCard={deleteCard}
                />
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
