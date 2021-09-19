import { FC, ReactElement } from "react";
import { Grid } from "@mui/material";
import InfoCard from "./InfoCard/InfoCard";
import { InfoCardData } from "./InfoCard/InfoCard";

export interface CardListProps {
    cards: InfoCardData[];
    editCard: (card: InfoCardData) => void;
    deleteCard: (key: InfoCardData["key"]) => void;
}

const CardList: FC<CardListProps> = ({ cards, editCard, deleteCard }) => {
    const renderCards = (): ReactElement[] => {
        return cards.map((card) => {
            return (
                <Grid item xs={12} sm={6} md={4} lg={3} key={card.key}>
                    <InfoCard
                        data={card}
                        editCard={editCard}
                        deleteCard={deleteCard}
                    />
                </Grid>
            );
        });
    };

    return (
        <Grid container spacing={4} padding={4}>
            {renderCards()}
        </Grid>
    );
};

export default CardList;
