import { Box } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import CardIconButton from "../CardIconButton";
import React from "react";

interface CardActionsButtonGroupProps {
    onClickEdit: React.EventHandler<React.MouseEvent>;
    onClickDelete: React.EventHandler<React.MouseEvent>;
}

const CardActionsButtonGroup: React.FC<CardActionsButtonGroupProps> = ({
    onClickEdit,
    onClickDelete,
}) => {
    return (
        <Box
            className="card-actions"
            sx={{ position: "absolute", top: 5, right: 5 }}
        >
            <CardIconButton
                icon={<EditIcon />}
                aria-label="edit"
                onClick={onClickEdit}
            />
            <CardIconButton
                icon={<DeleteIcon />}
                aria-label="delete"
                onClick={onClickDelete}
            />
        </Box>
    );
};

export default CardActionsButtonGroup;
