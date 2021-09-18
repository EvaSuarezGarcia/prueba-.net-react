import { FC, ReactElement } from "react";
import { Grid } from "@mui/material";
import InfoCard from "./InfoCard/InfoCard";
import { Props as CardProps } from "./InfoCard/InfoCard";

export interface Props {
    cards: CardProps[];
}

const CardList: FC<Props> = ({ cards }) => {
    const renderCards = (): ReactElement[] => {
        return cards.map((card) => {
            return (
                <Grid item xs={12} sm={6} md={4} lg={3}>
                    <InfoCard
                        title={card.title}
                        description={card.description}
                        image={card.image}
                    />
                </Grid>
            );
        });
    };

    return (
        <Grid container spacing={4} padding={4} bgcolor={"grey.200"}>
            {renderCards()}
        </Grid>
    );
};

export default CardList;
