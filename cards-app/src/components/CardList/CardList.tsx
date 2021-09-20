import { FC, ReactElement } from "react";
import { Grid } from "@mui/material";
import InfoCard from "./InfoCard/InfoCard";
import { InfoCardData } from "./InfoCard/InfoCard";
import { Box } from "@mui/system";
import SortButton from "../Buttons/SortButton";
import * as Constants from "../../Constants";

export interface CardListProps {
    cards: InfoCardData[];
    editCard: (card: InfoCardData) => void;
    deleteCard: (key: InfoCardData["key"]) => void;
    sortByTitle: () => void;
    sortByTitleAsc: boolean | null;
    sortByCreationDate: () => void;
    sortByCreationDateAsc: boolean | null;
}

const CardList: FC<CardListProps> = ({
    cards,
    editCard,
    deleteCard,
    sortByTitle,
    sortByTitleAsc,
    sortByCreationDate,
    sortByCreationDateAsc,
}) => {
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
        <Box padding={4}>
            <Grid container spacing={2} marginBottom={2}>
                <Grid item>
                    <SortButton
                        ascOrder={sortByTitleAsc}
                        triggerSort={sortByTitle}
                        text={Constants.SORT_BY_TITLE}
                    />
                </Grid>
                <Grid item>
                    <SortButton
                        ascOrder={sortByCreationDateAsc}
                        triggerSort={sortByCreationDate}
                        text={Constants.SORT_BY_CREATION_DATE}
                    />
                </Grid>
            </Grid>

            <Grid container spacing={4}>
                {renderCards()}
            </Grid>
        </Box>
    );
};

export default CardList;
