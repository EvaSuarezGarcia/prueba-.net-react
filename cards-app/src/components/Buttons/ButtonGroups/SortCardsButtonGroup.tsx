import { Grid } from "@mui/material";
import React from "react";
import SortButton from "../SortButton";
import * as Constants from "../../../Constants";

interface SortCardsButtonGroupProps {
    sortByTitleAsc: boolean | null;
    sortByTitle: () => void;
    sortByCreationDateAsc: boolean | null;
    sortByCreationDate: () => void;
}

const SortCardsButtonGroup: React.FC<SortCardsButtonGroupProps> = ({
    sortByTitleAsc,
    sortByTitle,
    sortByCreationDateAsc,
    sortByCreationDate,
}) => {
    return (
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
    );
};

export default SortCardsButtonGroup;
