import React from "react";
import { Box, createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import CardList from "./CardList/CardList";
import CardFormDialog from "./Dialogs/FormDialog/CardFormDialog";
import { CardListProps } from "./CardList/CardList";
import { InfoCardData } from "./CardList/InfoCard/InfoCard";
import AddFab from "./Buttons/AddFab";
import * as Constants from "../Constants";
import SortCardsButtonGroup from "./Buttons/ButtonGroups/SortCardsButtonGroup";

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

    // --- Sort cards ---
    const [sortByTitleAsc, setSortByTitleAsc] = React.useState<boolean | null>(
        null
    );

    const [sortByCreationDateAsc, setSortByCreationDateAsc] = React.useState<
        boolean | null
    >(null);

    const defaultTitleOrdering = true;

    const sortByTitle = () => {
        // Change ordering
        const ascOrder =
            sortByTitleAsc !== null ? !sortByTitleAsc : defaultTitleOrdering;
        const order = ascOrder ? 1 : -1;
        const newCards = [...cards].sort((a, b) =>
            a.title < b.title ? -1 * order : 1 * order
        );
        setCards(newCards);
        setSortByTitleAsc((prevState) =>
            prevState !== null ? !prevState : defaultTitleOrdering
        );
        setSortByCreationDateAsc(null);
    };

    const defaultDateOrdering = false;

    const sortByCreationDate = () => {
        const ascOrder =
            sortByCreationDateAsc !== null
                ? !sortByCreationDateAsc
                : defaultDateOrdering;
        const order = ascOrder ? 1 : -1;
        const newCards = [...cards].sort((a, b) =>
            a.creationDate < b.creationDate ? -1 * order : 1 * order
        );
        setCards(newCards);
        setSortByCreationDateAsc((prevState) =>
            prevState !== null ? !prevState : defaultDateOrdering
        );
        setSortByTitleAsc(null);
    };

    return (
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <Box padding={4}>
                <SortCardsButtonGroup
                    sortByTitleAsc={sortByTitleAsc}
                    sortByTitle={sortByTitle}
                    sortByCreationDateAsc={sortByCreationDateAsc}
                    sortByCreationDate={sortByCreationDate}
                />
                <CardList
                    cards={cards}
                    editCard={editCard}
                    deleteCard={deleteCard}
                />
            </Box>
            <AddFab onClick={handleClickOpenAddDialog} />
            <CardFormDialog
                open={showAddDialog}
                callback={addCard}
                handleClose={handleCloseAddDialog}
                dialogTitle={Constants.NEW_CARD}
                dialogButton={Constants.ADD}
                clearOnSubmit={true}
                initialCardData={{
                    title: "",
                    description: "",
                    image: "",
                    key: 0,
                    creationDate: 0,
                }}
            />
        </ThemeProvider>
    );
};

export default App;
